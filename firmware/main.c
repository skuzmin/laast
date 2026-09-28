#include <stdlib.h>
#include <string.h>

#include "pico/stdlib.h"
#include "pico/bootrom.h"
#include "tusb.h"
#include "usb_descriptors.h"
#include "led.h"

typedef enum
{
  USB_NOT_MOUNTED,
  USB_MOUNTED,
  USB_SUSPENDED,
} usb_state_t;

static volatile usb_state_t usb_state = USB_NOT_MOUNTED;

void hid_task(void);
void cdc_task(void);
void led_status_task(void);

int main(void)
{
  led_init();
  tud_init(BOARD_TUD_RHPORT);

  while (1)
  {
    tud_task();
    led_status_task();
    cdc_task();
    hid_task();
  }
}

void hid_task(void)
{
  static bool done = false;
  static int step = 0;
  static uint32_t mounted_at = 0;
  static uint32_t wait_until = 0;

  if (done || !tud_mounted()) return;

  uint32_t now = to_ms_since_boot(get_absolute_time());
  if (mounted_at == 0) mounted_at = now;
  if (now - mounted_at < 2000) return;   // wait 2 s after mount
  if (now < wait_until) return;          // wait between steps

  if (!tud_hid_ready()) return;          // previous report not sent yet

  uint8_t keys[6] = { HID_KEY_F1, 0, 0, 0, 0, 0 };

  switch (step)
  {
    case 0: // mouse left down
      tud_hid_mouse_report(REPORT_ID_MOUSE, MOUSE_BUTTON_LEFT, 0, 0, 0, 0);
      wait_until = now + 50;             // hold click 50 ms
      break;

    case 1: // mouse left up
      tud_hid_mouse_report(REPORT_ID_MOUSE, 0, 0, 0, 0, 0);
      wait_until = now + 500;            // pause 500 ms before F1
      break;

    case 2: // F1 down
      tud_hid_keyboard_report(REPORT_ID_KEYBOARD, 0, keys);
      wait_until = now + 50;            // hold F1 100 ms
      break;

    case 3: // F1 up
      tud_hid_keyboard_report(REPORT_ID_KEYBOARD, 0, NULL);
      done = true;                       // finished
      break;
  }

  step++;
}

void led_status_task(void)
{
  static int last = -1;
  if (last == usb_state)
  {
    return;
  }
  last = usb_state;

  switch (usb_state)
  {
  case USB_NOT_MOUNTED:
    led_set(32, 0, 0);
    break;
  case USB_MOUNTED:
    led_set(0, 32, 0);
    break;
  case USB_SUSPENDED:
    led_set(0, 0, 8);
    break;
  }
}

void cdc_task(void)
{
  if(tud_cdc_available())
  {
    char buf[64];
    uint32_t n = tud_cdc_read(buf, sizeof(buf));
    tud_cdc_write(buf, n);
    tud_cdc_write_flush();
  }
}

#pragma region USB state callbacks
void tud_mount_cb(void)
{
  usb_state = USB_MOUNTED;
}

void tud_umount_cb(void)
{
  usb_state = USB_NOT_MOUNTED;
}

void tud_suspend_cb(bool remote_wakeup_en)
{
  (void)remote_wakeup_en;
  usb_state = USB_SUSPENDED;
}

void tud_resume_cb(void)
{
  usb_state = tud_mounted() ? USB_MOUNTED : USB_NOT_MOUNTED;
}
#pragma endregion USB state callbacks

#pragma region HID callbacks
uint16_t tud_hid_get_report_cb(uint8_t instance, uint8_t report_id,
                               hid_report_type_t report_type,
                               uint8_t *buffer, uint16_t reqlen)
{
  (void)instance;
  (void)report_id;
  (void)report_type;
  (void)buffer;
  (void)reqlen;
  return 0;
}

void tud_hid_set_report_cb(uint8_t instance, uint8_t report_id,
                           hid_report_type_t report_type,
                           uint8_t const *buffer, uint16_t bufsize)
{
  (void)instance;
  (void)report_id;
  (void)report_type;
  (void)buffer;
  (void)bufsize;
}
#pragma endregion HID callbacks
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

#pragma region Keyboard/Mouse handlers

#pragma endregion Keyboard/Mouse handlers

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
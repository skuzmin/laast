#include "keyboard.h"
#include "tusb.h"
#include "pico/stdlib.h"
#include "usb_descriptors.h"
#include "time_utils.h"

#define PRESS_MS 50

typedef enum
{
  IDLE,
  PRESS,
  RELEASE
} state_t;

static state_t state = IDLE;
static uint8_t keycode = 0;
static uint32_t release_at = 0;

bool keyboard_press(uint8_t key)
{
  if (state != IDLE)
  {
    return false;
  }
  keycode = key;
  state = PRESS;
  return true;
}

void keyboard_task(void)
{
  if (state == IDLE || !tud_hid_ready())
  {
    return;
  }

  switch (state)
  {
  case PRESS:
  {
    uint8_t keys[6] = { keycode, 0, 0, 0, 0, 0 };
    tud_hid_keyboard_report(REPORT_ID_KEYBOARD, 0, keys);
    release_at = now_ms() + PRESS_MS;
    state = RELEASE;
    break;
  }

  case RELEASE:
    if (now_ms() < release_at)
    {
      return;
    }
    tud_hid_keyboard_report(REPORT_ID_KEYBOARD, 0, NULL);
    state = IDLE;
    break;

  default:
    state = IDLE;
    break;
  }
}
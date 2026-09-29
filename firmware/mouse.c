#include "mouse.h"
#include "tusb.h"
#include "pico/stdlib.h"
#include "usb_descriptors.h"
#include "time_utils.h"

#define CLICK_MS 50

typedef enum
{
  IDLE,
  PRESS,
  RELEASE,
  SCROLL
} state_t;

static state_t state = IDLE;
static uint8_t btn = 0;
static int8_t wheel = 0;
static uint32_t release_at = 0;

bool mouse_click(uint8_t button)
{
  if (state != IDLE)
  {
    return false;
  }
  btn = button;
  state = PRESS;
  return true;
}

bool mouse_scroll(int8_t delta)
{
  if (state != IDLE)
  {
    return false;
  }
  wheel = delta;
  state = SCROLL;
  return true;
}

void mouse_task(void)
{
  if (state == IDLE || !tud_hid_ready())
  {
    return;
  }

  switch (state)
  {
  case PRESS:
    tud_hid_mouse_report(REPORT_ID_MOUSE, btn, 0, 0, 0, 0);
    release_at = now_ms() + CLICK_MS;
    state = RELEASE;
    break;

  case RELEASE:
    if (now_ms() < release_at)
    {
      return;
    }
    tud_hid_mouse_report(REPORT_ID_MOUSE, 0, 0, 0, 0, 0);
    state = IDLE;
    break;

  case SCROLL:
    tud_hid_mouse_report(REPORT_ID_MOUSE, 0, 0, 0, wheel, 0);
    state = IDLE;
    break;

  default:
    state = IDLE;
    break;
  }
}
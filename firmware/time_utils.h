#pragma once
#include "pico/stdlib.h"

static inline uint32_t now_ms(void)
{
  return to_ms_since_boot(get_absolute_time());
}
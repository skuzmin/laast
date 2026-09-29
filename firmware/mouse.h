#pragma once
#include <stdint.h>
#include <stdbool.h>

bool mouse_click(uint8_t button);
bool mouse_scroll(int8_t delta);
void mouse_task(void);
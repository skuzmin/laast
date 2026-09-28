#include "led.h"
#include "hardware/pio.h"
#include "ws2812.pio.h"

#define LED_PIN 16

static PIO  pio = pio0;
static uint sm  = 0;

void led_init(void) {
    uint offset = pio_add_program(pio, &ws2812_program);
    ws2812_program_init(pio, sm, offset, LED_PIN, 800000);
}

void led_set(uint8_t r, uint8_t g, uint8_t b) {
    uint32_t grb = ((uint32_t)r << 16) | ((uint32_t)g << 8) | b;
    pio_sm_put_blocking(pio, sm, grb << 8u);
}

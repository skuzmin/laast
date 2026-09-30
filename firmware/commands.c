#include <string.h>
#include "tusb.h"
#include "commands.h"
#include "mouse.h"
#include "keyboard.h"
#include "protocol.h"

static bool session = false;

void commands_reset_session(void)
{
  session = false;
}

static void reply(const char *text)
{
  tud_cdc_write_str(text);
  tud_cdc_write_str("\n");
  tud_cdc_write_flush();
}

static void handle_mouse(const char *action, const char *arg)
{
  static const struct
  {
    const char *name;
    uint8_t button;
  } buttons[] = {
      {BTN_LEFT, MOUSE_BUTTON_LEFT},
      {BTN_RIGHT, MOUSE_BUTTON_RIGHT},
      {BTN_MIDDLE, MOUSE_BUTTON_MIDDLE},
      {BTN_BACK, MOUSE_BUTTON_BACKWARD},
      {BTN_FORWARD, MOUSE_BUTTON_FORWARD},
  };

  if (!action)
  {
    reply(ERR_MISSING_ACTION);
    return;
  }

  for (size_t i = 0; i < TU_ARRAY_SIZE(buttons); i++)
  {
    if (strcmp(action, buttons[i].name) == 0)
    {
      reply(mouse_click(buttons[i].button) ? RESP_OK : RESP_BUSY);
      return;
    }
  }

  if (strcmp(action, CMD_WHEEL) == 0)
  {
    if (arg && strcmp(arg, ARG_UP) == 0)
    {
      reply(mouse_scroll(1) ? RESP_OK : RESP_BUSY);
    }
    else if (arg && strcmp(arg, ARG_DOWN) == 0)
    {
      reply(mouse_scroll(-1) ? RESP_OK : RESP_BUSY);
    }
    else
    {
      reply(ERR_WHEEL_ARG);
    }
    return;
  }

  reply(ERR_UNKNOWN_MOUSE);
}

static void handle_key(const char *name)
{
  static const struct
  {
    const char *name;
    uint8_t key;
  } keys[] = {
      {KEY_F1, HID_KEY_F1},
      {KEY_F2, HID_KEY_F2},
      {KEY_F3, HID_KEY_F3},
      {KEY_F4, HID_KEY_F4},
      {KEY_F5, HID_KEY_F5},
      {KEY_F6, HID_KEY_F6},
      {KEY_F7, HID_KEY_F7},
      {KEY_F8, HID_KEY_F8},
      {KEY_F9, HID_KEY_F9},
      {KEY_F10, HID_KEY_F10},
      {KEY_F11, HID_KEY_F11},
      {KEY_F12, HID_KEY_F12},
      {KEY_ENTER, HID_KEY_ENTER},
      {KEY_ESC, HID_KEY_ESCAPE},
      {KEY_SPACE, HID_KEY_SPACE},
      {KEY_TAB, HID_KEY_TAB},
      {KEY_BACKSPACE, HID_KEY_BACKSPACE},
      {KEY_DELETE, HID_KEY_DELETE},
      {KEY_INSERT, HID_KEY_INSERT},
      {KEY_HOME, HID_KEY_HOME},
      {KEY_END, HID_KEY_END},
      {KEY_PAGEUP, HID_KEY_PAGE_UP},
      {KEY_PAGEDOWN, HID_KEY_PAGE_DOWN},
      {KEY_UP, HID_KEY_ARROW_UP},
      {KEY_DOWN, HID_KEY_ARROW_DOWN},
      {KEY_LEFT, HID_KEY_ARROW_LEFT},
      {KEY_RIGHT, HID_KEY_ARROW_RIGHT},
  };

  if (!name)
  {
    reply(ERR_MISSING_KEY);
    return;
  }

  if (strlen(name) == 1)
  {
    char c = name[0];
    uint8_t key = 0;

    if (c >= 'A' && c <= 'Z')
    {
      key = HID_KEY_A + (c - 'A');
    }
    else if (c >= '1' && c <= '9')
    {
      key = HID_KEY_1 + (c - '1');
    }
    else if (c == '0')
    {
      key = HID_KEY_0;
    }

    if (key)
    {
      reply(keyboard_press(key) ? RESP_OK : RESP_BUSY);
      return;
    }
  }

  for (size_t i = 0; i < TU_ARRAY_SIZE(keys); i++)
  {
    if (strcmp(name, keys[i].name) == 0)
    {
      reply(keyboard_press(keys[i].key) ? RESP_OK : RESP_BUSY);
      return;
    }
  }

  reply(ERR_UNKNOWN_KEY);
}

void handle_command(char *line)
{
  char *group = strtok(line, " ");
  char *action = strtok(NULL, " ");
  char *arg = strtok(NULL, " ");

  if (!group)
  {
    return;
  }

  if (strcmp(group, CMD_HELLO) == 0)
  {
    session = true;
    reply(RESP_HELLO);
    return;
  }

  if (!session)
  {
    reply(ERR_NOT_CONNECTED);
    return;
  }

  if (strcmp(group, CMD_MOUSE) == 0)
  {
    handle_mouse(action, arg);
    return;
  }

  if (strcmp(group, CMD_KEY) == 0)
  {
    handle_key(action);
    return;
  }

  if (strcmp(group, CMD_VERSION) == 0)
  {
    reply(FW_VERSION);
    return;
  }

  reply(ERR_UNKNOWN_COMMAND);
}
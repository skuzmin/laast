#include <string.h>
#include "tusb.h"
#include "commands.h"
#include "mouse.h"
#include "protocol.h"

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

void handle_command(char *line)
{
  char *group = strtok(line, " ");
  char *action = strtok(NULL, " ");
  char *arg = strtok(NULL, " ");

  if (!group)
  {
    return;
  }

  if (strcmp(group, CMD_MOUSE) == 0)
  {
    handle_mouse(action, arg);
    return;
  }

  reply(ERR_UNKNOWN_COMMAND);
}
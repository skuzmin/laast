#pragma once

#include "protocol.h"

#define RESP_HELLO            "HELLO LAAST " PROTOCOL_VERSION
#define ERR_NOT_CONNECTED     "ERR not connected"
#define RESP_OK               "OK"
#define RESP_BUSY             "BUSY"
#define ERR_UNKNOWN_COMMAND   "ERR unknown command"
#define ERR_MISSING_ACTION    "ERR missing action"
#define ERR_UNKNOWN_MOUSE     "ERR unknown mouse action"
#define ERR_WHEEL_ARG         "ERR wheel needs UP or DOWN"
#define ERR_MISSING_KEY       "ERR missing key"
#define ERR_UNKNOWN_KEY       "ERR unknown key"
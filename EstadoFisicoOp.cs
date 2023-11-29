﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EstadoFisicoOp
    {
        BuenEstado,
        PinturaRayada,
        ServoAtascado
    }
}
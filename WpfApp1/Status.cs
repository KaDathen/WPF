﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    internal class Status
    {
        public static bool Auth { get; set; }
        public static string Login { get; set; }
        public static bool Admin { get; set; }

        public static string text
        {
            get
            {
                return (Admin) ? "Админ" : "Менеджер";
            }

        }
    }
}

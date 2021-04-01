﻿//Purpose: Model - a class that represents a database table

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}

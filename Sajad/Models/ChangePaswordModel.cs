﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Models
{
    public class ChangePaswordModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}

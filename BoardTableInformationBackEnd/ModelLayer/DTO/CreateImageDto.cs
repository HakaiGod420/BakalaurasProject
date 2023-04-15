﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CreateImageDto
    {
        public string? TabletopTitle { get; set; }
        public List<string> FileNames { get; set; } = new List<string>();
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}

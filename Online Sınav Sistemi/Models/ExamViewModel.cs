﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Sınav_Sistemi.Models
{
    public class ExamViewModel
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
﻿using Microsoft.WindowsAzure.Mobile.Service;

namespace ChembetiMobileService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
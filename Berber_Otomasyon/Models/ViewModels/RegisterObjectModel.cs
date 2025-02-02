﻿namespace Berber_Otomasyon.Models.ViewModels
{
    public class RegisterObjectModel
    {
        public AddCalisanViewModel addCalisanViewModel { get; set; }

        public RegisterViewModel registerViewModel { get; set; }

        public SelectedArrays selectedArrays { get; set; }

        public RegisterObjectModel() {
            addCalisanViewModel = new AddCalisanViewModel();
            registerViewModel = new RegisterViewModel();
            selectedArrays = new SelectedArrays();
        }

    }
}

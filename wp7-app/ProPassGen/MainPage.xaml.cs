/**
 * Copyright (C) 2011 Lucas Maystre <lucas@maystre.ch>
 *
 * This file is part of propassgen.
 *
 * propassgen is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * propassgen is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with propassgen.  If not, see <http://www.gnu.org/licenses/>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace ProPassGen
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        public string GeneratePassword(int length)
        {
            // We just assume length is > 3.
            string password = "";

            var freqs = (App.Current as App).frequencies;
            var total = (App.Current as App).total;
            var symbs = (App.Current as App).symbols;

            Random rand = new Random();
            int counter = 0;
            int pointer = rand.Next(total) + 1;
            foreach (var freq in freqs)
            {
                counter += freq.Value;
                if (counter >= pointer)
                {
                    password = freq.Key;
                    break;
                }
            }

            while (password.Length < length)
            {
                char x = password[password.Length - 2];
                char y = password[password.Length - 1];

                int subtot = 0;
                foreach (var symb in symbs)
                {
                    subtot += freqs["" + x + y + symb];
                }

                counter = 0;
                pointer = rand.Next(subtot) + 1;
                foreach (var symb in symbs)
                {
                    counter += freqs["" + x + y + symb];
                    if (counter >= pointer) {
                        password += symb;
                        break;
                    }
                }
            }
            return password;
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            Pw0TextBlock.Text = this.GeneratePassword(10);
            Pw1TextBlock.Text = this.GeneratePassword(10);
            Pw2TextBlock.Text = this.GeneratePassword(10);
        }
    }
}

//
//  Copyright (c) 2009 EmergingSoft Corporation
//  http://www.emergingsoft.com/
//
//  By Ryan Morlok
//  http://softwareblog.morlok.net/
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace TestRegex
{
    class RegexTester : DependencyObject
    {
        public static readonly DependencyProperty RegexTextProperty
            = DependencyProperty.Register("RegexText", typeof(string), typeof(RegexTester));
        public static readonly DependencyProperty RegexTestStringProperty
            = DependencyProperty.Register("RegexTestString", typeof(string), typeof(RegexTester));
        public string RegexText
        {
            get { return (string)GetValue(RegexTextProperty); }
            set { SetValue(RegexTextProperty, value); }
        }

        public string RegexTestString
        {
            get { return (string)GetValue(RegexTestStringProperty); }
            set { SetValue(RegexTestStringProperty, value); }
        }

        private ObservableCollection<KeyValuePair<string, string>> _Matches
            = new ObservableCollection<KeyValuePair<string, string>>();

        public ObservableCollection<KeyValuePair<string, string>> Matches
        {
            get { return _Matches; }
        }

        public RegexTester()
        {
            DependencyPropertyDescriptor dpd1 = DependencyPropertyDescriptor.FromProperty(RegexTextProperty, typeof(RegexTester));
            if (dpd1 != null)
            {
                dpd1.AddValueChanged(this, delegate
                {
                    UpdateResults();
                });
            }

            DependencyPropertyDescriptor dpd2 = DependencyPropertyDescriptor.FromProperty(RegexTestStringProperty, typeof(RegexTester));
            if (dpd2 != null)
            {
                dpd2.AddValueChanged(this, delegate
                {
                    UpdateResults();
                });
            }
        }

        private void UpdateResults()
        {
            if (null != RegexText && null != RegexTestString)
            {
                try
                {
                    _Matches.Clear();
                    Regex r = new Regex(RegexText);
                    if (r.IsMatch(RegexTestString))
                    {
                        Match m = r.Match(RegexTestString);
                        for (int i = 0; i < m.Groups.Count; i++)
                        {
                            _Matches.Add(new KeyValuePair<string, string>((null != r.GroupNameFromNumber(i) ? r.GroupNameFromNumber(i) : i.ToString()), m.Groups[i].Value));
                        }
                        
                    } 
                }
                catch
                {
                    _Matches.Clear();
                }
            }
            else
            {
                _Matches.Clear();
            }
        }
    }
}

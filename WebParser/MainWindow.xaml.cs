using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HtmlAgilityPack;

namespace WebParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            contentTextBlock.Clear();

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(urlTextBox.Text);
                HtmlNode[] nodes = document.DocumentNode.SelectNodes("//p").ToArray();
                foreach (HtmlNode item in nodes)
                {
                    contentTextBlock.AppendText(item.InnerHtml + "\n");
                }

                string[] words = contentTextBlock.Text.Split(new string[] { " ", ",", ".", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                contentTextBlock.Clear();

                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                for (int i = 0; i < words.Length; i++)
                {
                    dictionary.Add(i, words[i]);
                }

                int k = 0;
                Dictionary<int, string> newDictionary = new Dictionary<int, string>();

                for (int i = 0; i < dictionary.Count; i++)
                {
                    if (!newDictionary.ContainsValue(dictionary[i]))
                    {
                        newDictionary.Add(k++, dictionary[i]);
                    }
                }

                int count = 0;
                for (int i = 0; i < newDictionary.Count; i++)
                {
                    if (newDictionary[i] == " ")
                        continue;
                    for (int j = 0; j < dictionary.Count; j++)
                    {
                        if (newDictionary[i] == dictionary[j])
                            count++;
                    }
                    contentTextBlock.AppendText
                        ($"{(i + 1).ToString().PadRight(5)} {newDictionary[i].PadRight(35)} {count}\n");
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

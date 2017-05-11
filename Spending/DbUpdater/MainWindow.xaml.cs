using DbUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Configuration Config { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            List<string> keys = this.Config.AppSettings.Settings.AllKeys.ToList();
            
            this.comboBox.ItemsSource = keys;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedValue != null && !string.IsNullOrEmpty(comboBox.SelectedValue.ToString()))
            {
                var stringConnection = this.Config.AppSettings.Settings[comboBox.SelectedValue.ToString()].Value.ToString();

                MessageBoxResult messageBoxResult = MessageBox.Show("Deseja realmente atualizar esta base de dados?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var upgrader = DeployChanges.To
                    .SqlDatabase(@stringConnection)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                    var result = upgrader.PerformUpgrade();

                    if (!result.Successful)
                    {
                        System.Windows.Forms.MessageBox.Show(result.Error.Message);
                    }
                    else
                    {
                        if (result.Scripts.Count() > 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Base de dados: " + comboBox.SelectedValue.ToString() + " atualizada com sucesso");
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Não há novos scripts para executar");
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("selecione uma base de dados");
            }            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.button.IsEnabled = true;
        }
    }
}

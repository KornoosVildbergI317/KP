using ZooMail.Models;
using ZooMail.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;


namespace ZooMail.Species
{
    /// <summary>
    /// Логика взаимодействия для Species.xaml
    /// </summary>
    /// 


    public partial class Species : Window
    {
        List<Models.ModelSpecies> listSpecies;

        string pathDocument = Session.baseDir;
        public string extension = string.Empty;


        public Species(/*Аргумента нет*/)
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Action action = () =>
            {
                listSpecies = (new DBManager()).getSpeciesList();
                dataGridSpecies.ItemsSource = listSpecies;
            };

            Dispatcher.Invoke(action);
           
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridSpecies.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбрана порода для редактирования");
                return;
            }

            var ap = new DBManager().getSpeciesList();

            Models.ModelSpecies modelSpecies = null;
            foreach (var it in ap)
            {
                if (it.Id_species == listSpecies[selectedIndex].Id_species)
                {
                    modelSpecies = it;
                    break;
                }
            }

            var ce = new Species(/*modelSpecies*/);
            ce.ShowDialog();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var ce = new Species();
            ce.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridSpecies.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбрана порода для удаления");
                return;
            }


            if (MessageBox.Show("Вы уверены?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }


            var listModels = new DBManager().getSupplierList();
            foreach (var it in listModels)
            {
                if (it.Id_Species == listSpecies[selectedIndex].Id_species)
                {
                    MessageBox.Show("Невозможно удалить связанную запись!");
                    return;
                }
            }
            new DBManager().deleteSpecies(listSpecies[selectedIndex].Id_species);
            MessageBox.Show("Операция выполнена");
            listSpecies = (new DBManager()).getSpeciesList();
            dataGridSpecies.ItemsSource = listSpecies;
        }

        private void createExportDoc()
        {
            try
            {
                DBManager con = new DBManager();

                var modelSpecies = con.getSpeciesList();

                if (extension == string.Empty)
                {
                    MessageBox.Show("Не выбран тип экспортруемого файла");
                    return;
                }

                switch (extension)
                {
                    case (".docx"):
                        string pathDocumentDOCX = Session.baseDir + "породы животных" + extension;
                        DocX document = DocX.Create(pathDocumentDOCX);
                        Xceed.Document.NET.Paragraph paragraph = document.InsertParagraph();
                        paragraph.
                            AppendLine("Документ '" + "Отчет о породах животных" + "' создан " + DateTime.Now.ToShortDateString()).
                            Font("Time New Roman").
                            FontSize(16).Bold().Alignment = Alignment.left;

                        paragraph.AppendLine();
                        Xceed.Document.NET.Table doctable = document.AddTable(modelSpecies.Count + 1, 2);
                        doctable.Design = TableDesign.TableGrid;
                        doctable.TableCaption = "порода животного";

                        doctable.Rows[0].Cells[0].Paragraphs[0].Append("Порода животного").Font("Times New Roman").FontSize(14);

                        for (int i = 0; i < modelSpecies.Count; i++)
                        {
                            doctable.Rows[i + 1].Cells[0].Paragraphs[0].Append(modelSpecies[i].Title).Font("Times New Roman").FontSize(14);
                        }
                        document.InsertParagraph().InsertTableAfterSelf(doctable);
                        document.Save();
                        MessageBox.Show("Отчет успешно сформирован!");
                        Process.Start(pathDocumentDOCX);
                        break;

                    case (".xlsx"):
                        Excel.Application excel;
                        Excel.Workbook worKbooK;
                        Excel.Worksheet worKsheeT;
                        Excel.Range celLrangE;

                        string pathDocumentXLSX = Session.baseDir + "Породы животных" + extension;

                        try
                        {
                            excel = new Excel.Application();
                            excel.Visible = false;
                            excel.DisplayAlerts = false;
                            worKbooK = excel.Workbooks.Add(Type.Missing);


                            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                            worKsheeT.Name = "Породы животных";

                            worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
                            worKsheeT.Cells[1, 1] = "Породы животных";
                            worKsheeT.Cells.Font.Size = 15;

                            for (int i = 0; i < modelSpecies.Count; i++)
                            {
                                worKsheeT.Cells[i + 3, 1] = modelSpecies[i].Title;
                            }

                            worKbooK.SaveAs(pathDocumentXLSX); ;
                            worKbooK.Close();
                            excel.Quit();
                            MessageBox.Show("Отчет успешно сформирован!");
                            Process.Start(pathDocumentXLSX);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                        finally
                        {
                            worKsheeT = null;
                            celLrangE = null;
                            worKbooK = null;
                        }

                        break;


                    case (".pdf"):
                        string pathDocumentPDF = Session.baseDir + "Породы животных" + extension;
                        if (File.Exists(Session.baseDir + "Породы животных.docx"))
                        {
                            Word.Application appWord = new Word.Application();
                            var wordDocument = appWord.Documents.Open(Session.baseDir + "Породы животных.docx");
                            wordDocument.ExportAsFixedFormat(pathDocumentPDF, Word.WdExportFormat.wdExportFormatPDF);
                            MessageBox.Show("Отчет успешно сформирован!");
                            wordDocument.Close();
                            Process.Start(pathDocumentPDF);
                        }
                        else
                            MessageBox.Show("Сначала сформируйте отчет .docx");
                        break;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Отсутсвие Ms Office на компьютере. Пожалуйста скачайте его.");
                Process.Start("https://www.microsoft.com/ru-ru/microsoft-365/compare-all-microsoft-365-products?tab=1&rtc=1");
            }
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(createExportDoc));
            t.Start();
        }

        private void ComboBoxExport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)comboBoxExport.SelectedItem;
            extension = typeItem.Content.ToString();



        }
    }
}

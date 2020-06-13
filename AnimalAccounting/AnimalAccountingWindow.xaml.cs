using ZooMail.Models;
using ZooMail.AnimalAccounting;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Windows.Controls;

namespace ZooMail.AnimalAccounting
{
    /// <summary>
    /// Логика взаимодействия для AnimalAccountingWindow.xaml
    /// </summary>
    public partial class AnimalAccountingWindow : Window
    {
        async Task<bool> Symb(string str)
        {
            bool znach = false;
            await Task.Run(() =>
            {
                if (
                str.Contains("?") || str.Contains("!") || str.Contains("@") ||
                str.Contains("#") || str.Contains("№") || str.Contains("~") ||
                str.Contains(";") || str.Contains("%") || str.Contains("$") ||
                str.Contains("^") || str.Contains("&") || str.Contains(":") ||
                str.Contains("*") || str.Contains("(") || str.Contains(")") ||
                str.Contains("_") || str.Contains("=") || str.Contains("+") ||
                str.Contains("/") || str.Contains("|") || str.Contains("[") ||
                str.Contains("]") || str.Contains("{") || str.Contains("}") ||
                str.Contains("<") || str.Contains(">") || str.Contains("-") ||
                str.Contains(",") || str.Contains("`") || str.Contains("."))
                    znach = true;
            });
            return znach;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        List<Models.ModelAnimalparkDetail> listAnimal;

        public string extension = string.Empty;



        public AnimalAccountingWindow()
        {
            InitializeComponent();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridCars.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран товар для редактирования");
                return;
            }

            var ap = new DBManager().getAnimalparkList();

            Models.ModelAnimalpark modelAnimalpark = null;
            foreach (var it in ap)
            {
                if (it.ID_animalpark == listAnimal[selectedIndex].ID_animalpark)
                {
                    modelAnimalpark = it;
                    break;
                }
            }

            var ce = new AnimalEdit(modelAnimalpark);
            ce.ShowDialog();

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var ce = new AnimalEdit(null);
            ce.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            listAnimal = (new DBManager()).getAnimalparkListDetail();
            dataGridCars.ItemsSource = listAnimal;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridCars.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран товар для редактирования");
                return;
            }


            if (MessageBox.Show("Вы уверены?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }


            new DBManager().deleteAnimalpark(listAnimal[selectedIndex].ID_animalpark);
            MessageBox.Show("Операция выполнена");
            listAnimal = (new DBManager()).getAnimalparkListDetail();
            dataGridCars.ItemsSource = listAnimal;
        }

        private void createExportDoc()
        {
            try
            {


                DBManager con = new DBManager();

                var modelAnimalpark = con.getAnimalparkList();

                if (extension == string.Empty)
                {
                    MessageBox.Show("Не выбран тип экспортруемого файла");
                    return;
                }


                switch (extension)
                {
                    case (".docx"):
                        string pathDocumentDOCX = Session.baseDir + "Учет товар" + extension;
                        DocX document = DocX.Create(pathDocumentDOCX);
                        Xceed.Document.NET.Paragraph paragraph = document.InsertParagraph();
                        paragraph.
                            AppendLine("Документ '" + "Отчет о учете автомобилей" + "' создан " + DateTime.Now.ToShortDateString()).
                            Font("Time New Roman").
                            FontSize(16).Bold().Alignment = Alignment.left;

                        paragraph.AppendLine();
                        Xceed.Document.NET.Table doctable = document.AddTable(modelAnimalpark.Count + 1, 2);
                        doctable.Design = TableDesign.TableGrid;
                        doctable.TableCaption = "учет товара";

                        doctable.Rows[0].Cells[0].Paragraphs[0].Append("Учет товара").Font("Times New Roman").FontSize(14);

                        for (int i = 0; i < modelAnimalpark.Count; i++)
                        {
                            doctable.Rows[i + 1].Cells[0].Paragraphs[0].Append(modelAnimalpark[i].Number).Font("Times New Roman").FontSize(14);
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

                        string pathDocumentXLSX = Session.baseDir + "Учет о животных" + extension;

                        try
                        {
                            excel = new Excel.Application();
                            excel.Visible = false;
                            excel.DisplayAlerts = false;
                            worKbooK = excel.Workbooks.Add(Type.Missing);


                            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                            worKsheeT.Name = "Учет о животных";

                            worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
                            worKsheeT.Cells[1, 1] = "Учет о животных";
                            worKsheeT.Cells.Font.Size = 15;

                            for (int i = 0; i < modelAnimalpark.Count; i++)
                            {
                                worKsheeT.Cells[i + 3, 1] = modelAnimalpark[i].Number;
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
                        string pathDocumentPDF = Session.baseDir + "Учет о животных" + extension;
                        if (File.Exists(Session.baseDir + "Учет о животных.docx"))
                        {
                            Word.Application appWord = new Word.Application();
                            var wordDocument = appWord.Documents.Open(Session.baseDir + "Учет о животных.docx");
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

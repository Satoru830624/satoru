using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace PrimaFileIntegrationApp
{
    public partial class FileIntegrationForm : Form
    {
        DateTime dtToday = DateTime.Today;
        string[] PrimaFileJp;
        string[] PrimaFileWorld;
        List<string> BlackList = new List<string>();

        static string[] ProductLabel = { "ASIN", "出品者数", "最安値", "最安値送料", "商品重量(kg)", "商品名", "商品グループ", "メーカ名",
                                            "ブランド名", "発売日", "サイズ_高さ(cm)", "サイズ_長さ(cm)", "サイズ_幅(cm)", "アダルトフラグ",
                                            "US_出品者数", "US_最安値", "US_最安値送料", "US商品名", "US_商品グループ", "USメーカ名", "USブランド名", "US_アダルトフラグ", "2番手安値",};

        IDictionary<string, int> ProductInfoLabel = new Dictionary<string, int>();      //入力ファイル内のラベル(string)の位置(int)を格納する

        static XLWorkbook workbook = new XLWorkbook(@"C:\Users\sato_\Downloads\1.出品テンプレートファイル\出品禁止ブランド、キーワード.xlsx");
        static IXLWorksheet worksheet =  workbook.Worksheet(2);
        static int lastRow = worksheet.LastRowUsed().RowNumber();
        string[] ProhibitBrandWord = new string[lastRow + 1];

        public FileIntegrationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //操作なし
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //操作なし
        }

        /// <summary>
        /// ボタンを押すと統合したいファイルのあるフォルダパスを選択する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePathSelectButton_Click(object sender, EventArgs e)
        {
            // マイコンピュータをルートフォルダにする
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;

            folderBrowserDialog1.SelectedPath = @"C:\Users\sato_\Downloads\10.old\";

            // FolderBrowserDialogを表示
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePathTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// ボタンを押すとファイル統合/編集を実行する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunFileIntegrationButton_Click(object sender, EventArgs e)
        {
            var FileCategoryName = SelectFileCategoryName(folderBrowserDialog1.SelectedPath);
            var OutFileNamePath = folderBrowserDialog1.SelectedPath + "\\IntegratedFile" + FileCategoryName + ".csv";
            var EditAndIntegratedFileName = folderBrowserDialog1.SelectedPath + "\\" + dtToday.ToString("yyyyMMdd") + "_EditAndIntegratedFile" + FileCategoryName + ".csv";
            
            for(var i = 1; i <= lastRow;i++)
            {
                IXLCell cell = worksheet.Cell(i, 1);

                ProhibitBrandWord[i - 1] = (string)cell.Value;
            }

            //待機状態
            Cursor = Cursors.WaitCursor;

            using (var IntegratedFileWrite = new StreamWriter(OutFileNamePath, false, System.Text.Encoding.GetEncoding("sjis")))
            {
                for (var fileNum = 1; fileNum <= 3; fileNum++)
                {
                    PrimaFilesSelect(fileNum);

                    var inJapanFile = new StreamReader(PrimaFileJp[0], System.Text.Encoding.GetEncoding("sjis"));

                    var inWorldFile = new StreamReader(PrimaFileWorld[0], System.Text.Encoding.GetEncoding("sjis"));

                    PrimaFileIntegration(inJapanFile, inWorldFile, IntegratedFileWrite, fileNum);
                }
            }

            using (var IntegratedFileRead = new StreamReader(OutFileNamePath, System.Text.Encoding.GetEncoding("sjis")))
            {
                using (var EditAndIntegratedFileWrite = new StreamWriter(EditAndIntegratedFileName, false, System.Text.Encoding.GetEncoding("sjis")))
                {
                    EditIntegratedFile(IntegratedFileRead, EditAndIntegratedFileWrite);
                }
            }

            /*ファイル削除（不具合が出ないことを確認するまでしばらく止める）*/
            //File.Delete(OutFileNamePath);

            //待機状態解除
            Cursor = Cursors.Default;

            MessageBox.Show("ファイルの統合が正常に完了しました。", "統合完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        private void FilePathTextBox_TextChanged(object sender, EventArgs e)
        {
            //操作なし
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            //操作なし
        }

        /// <summary>
        /// 開くPRIMAファイルを選択する
        /// </summary>
        /// <param name="openFileTime">何回目のファイルオープンか</param>
        public void PrimaFilesSelect(int openFileTime)
        {
            switch (openFileTime)
            {
                case 1:
                    PrimaFileJp = Directory.GetFiles(@FilePathTextBox.Text, "PRIMA_JAPAN_*_01.txt.csv");
                    PrimaFileWorld = Directory.GetFiles(@FilePathTextBox.Text, "PRIMA_WORLD_*_01.txt.csv");
                    break;
                case 2:
                    PrimaFileJp = Directory.GetFiles(@FilePathTextBox.Text, "PRIMA_JAPAN_*_02.txt.csv");
                    PrimaFileWorld = Directory.GetFiles(@FilePathTextBox.Text, "PRIMA_WORLD_*_02.txt.csv");
                    break;
                case 3:
                    PrimaFileJp = Directory.GetFiles(@FilePathTextBox.Text, "PRIMA_JAPAN_*_03.txt.csv");
                    PrimaFileWorld = Directory.GetFiles(@FilePathTextBox.Text, "PRIMA_WORLD_*_03.txt.csv");
                    break;
            }
        }

        /// <summary>
        /// ファイル名にジャンルを加える
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <returns></returns>
        public string SelectFileCategoryName(string DirectoryName)
        {
            string iFileCategoryName;

            if (DirectoryName.IndexOf("_CD_used") >= 0)
            {
                iFileCategoryName = "_CD_used";
            }
            else if (DirectoryName.IndexOf("kakakusa_new") >= 0)
            {
                iFileCategoryName = "_new";
            }
            else if (DirectoryName.IndexOf("kakakusa_used") >= 0)
            {
                iFileCategoryName = "_used";
            }
            else
            {
                iFileCategoryName = "_noCategory";
            }

            return iFileCategoryName;
        }

        /// <summary>
        /// ファイルを統合する
        /// </summary>
        /// <param name="JpFile"></param>
        /// <param name="WorledFile"></param>
        /// <param name="writeFile"></param>
        /// <param name="openFileTime"></param>
        public void PrimaFileIntegration(StreamReader JpFile, StreamReader WorledFile, StreamWriter writeFile, int openFileTime)
        {
            string readJpFileLine;
            string readWorldFileLine;
            string buffer;

            //PRIMAファイル(JAPAN,WORLD)を1行ずつ読み込み、統合ファイルにコピーする（読み込んだデータがNULLになるまで）
            while ((readJpFileLine = JpFile.ReadLine()) != null && (readWorldFileLine = WorledFile.ReadLine()) != null)
            {
                int countJp = 0;
                int countWorld = 0;
                char c = '\"';

                //ダブルクウォート数が奇数の時、もう一行読み込む
                countJp = CountChar(readJpFileLine, c);
                countWorld = CountChar(readWorldFileLine, c);

                //CSVファイルの再読み込み（セル内に改行が含まれていた場合の対応）
                readJpFileLine = reReadFileLine(readJpFileLine, JpFile, countJp);
                readWorldFileLine = reReadFileLine(readWorldFileLine, WorledFile, countWorld);

                //JAPANファイルとWORLDファイルの情報を統合する
                buffer = readJpFileLine + "," + readWorldFileLine;

                //統合した情報を統合ファイルに書き込む
                writeFile.WriteLine(buffer);
            }
        }

        /// <summary>
        /// 文字列内の特定文字数をカウントする
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

        /// <summary>
        /// CSVファイルの再読み込み（セル内に改行が含まれていた場合の対応）
        /// </summary>
        /// <param name="FileLine"></param>
        /// <param name="readStream"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public string reReadFileLine(string FileLine, StreamReader readStream, int cnt)
        {
            string buffFileLine;

            if (cnt % 2 == 1)
            {
                buffFileLine = FileLine + readStream.ReadLine();
                //buffFileLineとFileLineが同じ文字数なら、もう一行存在する可能性があるので再々読み込み
                while (buffFileLine.Length == FileLine.Length)
                {
                    buffFileLine = FileLine + readStream.ReadLine();
                }
            }else
            {
                buffFileLine = FileLine;
            }

            return buffFileLine;
        }

        /// <summary>
        /// 統合ファイルのうち、不必要なデータを削除する
        /// </summary>
        /// <param name="Read">統合したファイルを読み込んだストリーム</param>
        /// <param name="Write">編集した結果を書き出すストリーム</param>
        public void EditIntegratedFile(StreamReader Read, StreamWriter Write)
        {
            string ReadFileLine;
            var FirstFlg = true; //1行目の商品情報かどうか
            var IsWritableData = true;

            while ((ReadFileLine = Read.ReadLine()) != null)
            {
                var ProductData = ReadFileLine.Split(',');

                if (FirstFlg == true)       //1行目の商品情報(ラベル)を取得し書き込む
                {
                    FirstFlg = false;

                    GetLabelIndex(ProductData);
                }
                else
                {
                    //書き込むかどうかの判断をする
                    IsWritableData = IsListableProductData(ProductData, ProhibitBrandWord);
                }

                if (IsWritableData == true)
                {
                    WriteToEditFile(Write, ProductData);

                    IsWritableData = false;
                }
            }
        }

        /// <summary>
        /// 商品情報の列番号を取得する
        /// </summary>
        /// <param name="iProductData">統合ファイルの情報(1行)</param>
        public void GetLabelIndex(string[] iProductData)
        {
            var count = 0;

            foreach(var value in iProductData)
            {
                foreach(var pl in ProductLabel)
                {
                    WriteLabelIndex(value, pl, count);
                }

                count++;
            }
        }

        /// <summary>
        /// 商品情報（ラベル）が記載されている列番号を商品情報テーブルに書き込む
        /// </summary>
        /// <param name="iValue">統合ファイルの情報</param>
        /// <param name="iProductLabel">商品ラベル</param>
        /// <param name="NowCount">列番号</param>
        public void WriteLabelIndex(string iValue, string iProductLabel, int NowCount)
        {
            if (iValue == iProductLabel)
            {
                try
                {
                    ProductInfoLabel[iProductLabel] = NowCount;
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show("必要な商品情報が統合ファイルに存在しません。", "入力データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }
            }
        }

        /// <summary>
        /// 編集した情報を書き込む
        /// </summary>
        /// <param name="streamWriter">書き込みファイルストリーム</param>
        /// <param name="iProductData">書き込む情報</param>
        public void WriteToEditFile(StreamWriter streamWriter, string[] iProductData)
        {
            string BuffProductData = "";

            foreach(var i in ProductLabel)
            {
                if (i.Equals("サイズ_高さ(cm)") || i.Equals("サイズ_長さ(cm)") || i.Equals("サイズ_幅(cm)") || i.Equals("商品名")          || i.Equals("商品グループ")          || i.Equals("メーカ名") || i.Equals("ブランド名") || i.Equals("発売日") || i.Equals("アダルトフラグ")
                || i.Equals("US商品名")            || i.Equals("US_商品グループ") || i.Equals("USメーカ名")      || i.Equals("USブランド名") || i.Equals("US_アダルトフラグ") || i.Equals("2番手安値"))
                {
                    //上記ラベルの値を取得しない
                    continue;
                }
                else if (i.Equals("ASIN")) 
                {
                    BuffProductData = iProductData[ProductInfoLabel[i]];
                }
                else
                {
                    BuffProductData += AddProductData(i, iProductData);
                }
            }

            streamWriter.WriteLine(BuffProductData);
        }

        /// <summary>
        /// 商品データをカンマ区切りで1行にまとめる
        /// </summary>
        /// <param name="iLabel">ラベル</param>
        /// <param name="iProductData">書き込む情報</param>
        /// <returns>1行にまとめたデータ</returns>
        public string AddProductData(string iLabel,string[] iProductData)
        {
            string ReturnData;

            try
            {
                if (ProductInfoLabel.ContainsKey(iLabel) == true)
                {
                    ReturnData = "," + iProductData[ProductInfoLabel[iLabel]];
                }
                else
                {
                    ReturnData = "";
                }
            }
            catch (KeyNotFoundException)
            {
                ReturnData = "";
            }

            return ReturnData;
        }

        /// <summary>
        /// 特定条件を満たす商品情報かを判断する
        /// </summary>
        /// <param name="iProductData">商品情報(データ)</param>
        /// <returns>判断結果</returns>
        public bool IsListableProductData(string[] iProductData, string[] ProhibitData)
        {
            //"ASIN"の値が"ASIN"でないデータを書き出す
            if (iProductData[ProductInfoLabel["ASIN"]].Equals("ASIN") == true)
            {
                return false;
            }

            //"アダルトフラグ"、"US_アダルトフラグ"が"TRUE"でないデータを書き出す。”TRUE”のデータをブラックリストに書き込む
            if (iProductData[ProductInfoLabel["アダルトフラグ"]].Equals("\"true\"", StringComparison.CurrentCultureIgnoreCase)
                || iProductData[ProductInfoLabel["US_アダルトフラグ"]].Equals("\"true\"", StringComparison.CurrentCultureIgnoreCase))
            {
                AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                return false;
            }

            //"出品者数"：3以上のデータを書き出す
            if (iProductData[ProductInfoLabel["出品者数"]].Equals("0") == true || iProductData[ProductInfoLabel["出品者数"]].Equals("1") == true || iProductData[ProductInfoLabel["出品者数"]].Equals("2") == true)
            {
                return false;
            }

            //"最安値"：0でないデータを書き出す
            if (iProductData[ProductInfoLabel["最安値"]].Equals("0") == true)
            {
                return false;
            }

            //"商品重量(kg)"：0でない、かつ、1.5以下のデータを書き出す
            if (iProductData[ProductInfoLabel["商品重量(kg)"]].Equals("0") == true || Convert.ToSingle(iProductData[ProductInfoLabel["商品重量(kg)"]]) > 1.5)
            {
                if(iProductData[ProductInfoLabel["商品重量(kg)"]].Equals("0") == true)
                {
                    AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);
                }

                return false;
            }

            //"サイズ_高さ(cm)"、"サイズ_長さ(cm)"、"サイズ_幅(cm)"の全てがゼロでない、もしくは合計が80以下のデータを書き出す
            if(ProductInfoLabel.ContainsKey("サイズ_高さ(cm)") == true && ProductInfoLabel.ContainsKey("サイズ_長さ(cm)") == true && ProductInfoLabel.ContainsKey("サイズ_幅(cm)") == true)
            {
                if (iProductData[ProductInfoLabel["サイズ_高さ(cm)"]].Equals("0")
                    || iProductData[ProductInfoLabel["サイズ_長さ(cm)"]].Equals("0")
                    || iProductData[ProductInfoLabel["サイズ_幅(cm)"]].Equals("0")
                    || Convert.ToSingle(iProductData[ProductInfoLabel["サイズ_高さ(cm)"]])
                        + Convert.ToSingle(iProductData[ProductInfoLabel["サイズ_長さ(cm)"]])
                        + Convert.ToSingle(iProductData[ProductInfoLabel["サイズ_幅(cm)"]]) > 80)
                {
                    AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                    return false;
                }
            }

            //今日より先の発売日商品は除去
            var StringToday = dtToday.ToString("yyyyMMdd");
            var IntToday = int.Parse(StringToday);
            if(String.IsNullOrEmpty(iProductData[ProductInfoLabel["発売日"]]) == false)
            {
                try
                {
                    if ((IntToday - int.Parse(iProductData[ProductInfoLabel["発売日"]])) < 0)
                    {
                        return false;
                    }
                }
                catch(Exception)
                {
                    //何もしない
                }
            }

            //"US_出品者数"が"0" or "1"でないデータを書き出す（著作権クレームを受けやすいデータの為）
            if (iProductData[ProductInfoLabel["US_出品者数"]].Equals("0") || iProductData[ProductInfoLabel["US_出品者数"]].Equals("1"))
            {
                return false;
            }

            //2番手安値が最安値の1.1倍以上なら、出品数を最低値（3）にする
            if(iProductData[ProductInfoLabel["2番手安値"]].Equals("0") == false)
            {
                int LowestPrice, SecondLowestPrice; //最安値、二番手安値
                bool IntFlgLowest = true, IntFlgSecond = true;//数値が取得できたかどうか

                IntFlgLowest = int.TryParse(iProductData[ProductInfoLabel["最安値"]], out LowestPrice);

                IntFlgSecond = int.TryParse(iProductData[ProductInfoLabel["2番手安値"]], out SecondLowestPrice);

                if (IntFlgLowest == true && IntFlgSecond == true && (LowestPrice * 1.1 < SecondLowestPrice))
                {
                    iProductData[ProductInfoLabel["出品者数"]] = "3";
                }
            }

            //商品名、ブランド名から、出品不可商品を除去し、ブラックリストに登録する
            foreach(var prohibit in ProhibitData){
                if(String.IsNullOrEmpty(prohibit) == false)
                {
                    var SearchWord = "\"" + prohibit.Trim() + "\"";     //商品名などは""で囲っているため、””で囲ってから検索する

                    if (iProductData[ProductInfoLabel["商品名"]].StartsWith(SearchWord, StringComparison.OrdinalIgnoreCase)) 
                    {
                        AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                        return false;
                    }

                    if(iProductData[ProductInfoLabel["メーカ名"]].StartsWith(SearchWord, StringComparison.OrdinalIgnoreCase))
                    {
                        AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                        return false;
                    }

                    if (iProductData[ProductInfoLabel["ブランド名"]].StartsWith(SearchWord, StringComparison.OrdinalIgnoreCase)) 
                    {
                        AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                        return false;
                    }

                    if (iProductData[ProductInfoLabel["US商品名"]].StartsWith(SearchWord, StringComparison.OrdinalIgnoreCase)) 
                    {
                        AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                        return false;
                    }

                    if (iProductData[ProductInfoLabel["USメーカ名"]].StartsWith(SearchWord, StringComparison.OrdinalIgnoreCase)) 
                    {
                        AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                        return false;
                    }

                    if(iProductData[ProductInfoLabel["USブランド名"]].StartsWith(SearchWord, StringComparison.OrdinalIgnoreCase))
                    {
                        AddBlackList(iProductData[ProductInfoLabel["ASIN"]]);

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ブラックリストを取得する
        /// </summary>
        public void GetBlackList()
        {
            string BlackAsinData;
            string BlackListFilePath = "C:\\Users\\sato_\\Downloads\\2.ブラックリスト\\BlackList.csv";

            using (var ReadBlackListFile = new StreamReader(BlackListFilePath, System.Text.Encoding.GetEncoding("sjis")))
            {
                while ((BlackAsinData = ReadBlackListFile.ReadLine()) != null)
                {
                    BlackList.Add(BlackAsinData);
                }

                ReadBlackListFile.Close();
            }
        }

        /// <summary>
        /// ブラックリストを追加する
        /// </summary>
        public void AddBlackList(string AsinCode)
        {
            string BlackListFilePath = "C:\\Users\\sato_\\Downloads\\2.ブラックリスト\\" + dtToday.ToString("yyyyMMdd") + "_NewBlackList.csv";

            using (var WriteBlackListFile = new StreamWriter(BlackListFilePath, true, System.Text.Encoding.GetEncoding("sjis")))
            {
                WriteBlackListFile.WriteLine(AsinCode);

                WriteBlackListFile.Close();
            }
        }

        /// <summary>
        /// ブラックリストに登録したASINでないかを判断する
        /// </summary>
        /// <param name="iProductData"></param>
        /// <returns>判断結果</returns>
        public bool IsNoBlackListASIN(string AsinCode)
        {
            if(BlackList.Contains(AsinCode) == true)
            {
                return false;
            }

            return true;
        }
    }
}
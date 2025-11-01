using Quokka;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.Windows.Media.Imaging;

namespace Plugin_UnicodeCharacterLookup {
  class CharItem : ListItem {

    public CharItem(string character, string name) {
      Name = character;
      Description = name;
      UiDispatcher.BeginInvoke(() => {
        Icon = new BitmapImage(new Uri(
            Environment.CurrentDirectory + "\\PlugBoard\\Plugin_UnicodeCharacterLookup\\Plugin\\globe.png"));
      });
    }

    //When item is selected, copy text
    public override void Execute() {
      System.Windows.Clipboard.SetText(Name);
      App.Current.MainWindow.Close();
    }
  }

}

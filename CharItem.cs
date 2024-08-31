using Quokka;
using Quokka.ListItems;
using System.Windows.Media.Imaging;

namespace Plugin_UnicodeCharacterLookup {
  class CharItem : ListItem {

    public CharItem(Char character) {
      this.Name = character.character;
      this.Description = character.description;
      this.Icon = new BitmapImage(new Uri(
          Environment.CurrentDirectory + "\\PlugBoard\\Plugin_UnicodeCharacterLookup\\Plugin\\globe.png"));
    }

    //When item is selected, copy text
    public override void Execute() {
      System.Windows.Clipboard.SetText(Name);
      App.Current.MainWindow.Close();
    }
  }

}

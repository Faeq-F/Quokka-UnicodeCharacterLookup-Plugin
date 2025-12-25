using Quokka;
using Quokka.ListItems;
using Quokka.PluginArch;

namespace PluginUnicodeCharacterLookup
{
  class CharItem : ListItem
  {

    public CharItem(string character, string name)
    {
      Name = character;
      Description = name;
      Icon = IconCache.GetOrAdd(
        Environment.CurrentDirectory + "\\PlugBoard\\PluginUnicodeCharacterLookup\\Plugin\\globe.png"
      );
    }

    //When item is selected, copy text
    public override void Execute()
    {
      System.Windows.Clipboard.SetText(Name);
      App.Current.MainWindow.Close();
    }
  }

}

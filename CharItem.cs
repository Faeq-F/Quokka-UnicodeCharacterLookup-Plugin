using Quokka.ListItems;
using Quokka.PluginArch;
using System.Windows;

namespace PluginUnicodeCharacterLookup
{
  internal sealed class CharItem : ListItem
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
      Clipboard.SetText(Name);
      Application.Current.MainWindow.Close();
    }
  }

}

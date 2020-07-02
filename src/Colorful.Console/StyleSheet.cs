using System.Collections.Generic;
using System.Drawing;

namespace Colorful
{
    /// <summary>
    /// Exposes a collection of style classifications which can be used to style text.
    /// </summary>
    public sealed class StyleSheet
    {
        /// <summary>
        /// The StyleSheet's collection of style classifications.
        /// </summary>
        public List<StyleClass<TextPattern>> Styles { get; private set; }
        /// <summary>
        /// The color to be associated with unstyled text.
        /// </summary>
        public Color UnstyledColor;

        /// <summary>
        /// Exposes a collection of style classifications which can be used to style text.
        /// </summary>
        /// <param name="defaultColor">The color to be associated with unstyled text.</param>
        public StyleSheet(Color defaultColor)
        {
            Styles = new List<StyleClass<TextPattern>>();
            UnstyledColor = defaultColor;
        }

        /// <summary>
        /// Adds a style classification to the StyleSheet.
        /// </summary>
        /// <param name="target">The string to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        /// <param name="matchHandler">A delegate instance which describes a transformation that
        /// can be applied to the target.</param>
        /// <param name="filterMatchesByIndex">An array contain index of the matches to filter.</param>
        public void AddStyle(string target, Color color, Styler.MatchFound matchHandler, params int[] filterMatchesByIndex)
        {
            Styler styler = new Styler(target, color, matchHandler, filterMatchesByIndex);

            Styles.Add(styler);
        }

        /// <summary>
        /// Adds a style classification to the StyleSheet.
        /// </summary>
        /// <param name="target">The string to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        /// <param name="matchHandler">A delegate instance which describes a simpler transformation that
        /// can be applied to the target.</param>
        /// <param name="filterMatchesByIndex">An array contain index of the matches to filter.</param>
        public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler, params int[] filterMatchesByIndex)
        {
            string Wrapper(string s, MatchLocation l, string m) => matchHandler.Invoke(m);
            Styler styler = new Styler(target, color, Wrapper, filterMatchesByIndex);

            Styles.Add(styler);
        }

        /// <summary>
        /// Adds a style classification to the StyleSheet.
        /// </summary>
        /// <param name="target">The string to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        /// <param name="filterMatchesByIndex">An array contain index of the matches to filter.</param>
        public void AddStyle(string target, Color color, params int[] filterMatchesByIndex)
        {
            string Handler(string s, MatchLocation l, string m) => m;
            Styler styler = new Styler(target, color, Handler, filterMatchesByIndex);

            Styles.Add(styler);
        }
    }
}

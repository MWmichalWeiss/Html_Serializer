using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace praktikod
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public StringBuilder InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }


        public IEnumerable<HtmlElement> Descendants()
        {
            HtmlElement tempFather;
            Queue<HtmlElement> queueElements = new Queue<HtmlElement>();

            queueElements.Enqueue(this);

            while (queueElements.Count != 0)
            {
                tempFather = queueElements.Dequeue();

                yield return tempFather;

                if(tempFather.Children!= null)
                {
                    foreach (var item in tempFather.Children)
                    {
                        queueElements.Enqueue(item);
                    }
                }
               
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement root = this;
            List<HtmlElement> allFather = new List<HtmlElement>();
            while (root != null)
            {
                allFather.Add(root);
                root = root.Parent;
            }
            return allFather;
        }



        public List<HtmlElement> FindElementsUsingSelector(Selector selector)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            FindElementsRecursively(this, selector, result);
            return result.ToList();
        }

        private void FindElementsRecursively(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
        {
            if (selector== null)
                return;

            foreach (HtmlElement descendant in element.Descendants())
            {
                if (MatchesSelector(descendant, selector))
                {
                    if (selector.Child == null)
                    {
                        result.Add(descendant);
                    }
                    else
                    {
                        FindElementsRecursively(descendant, selector.Child, result);
                    }
                }
            }
        }


        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            bool matchesClasses = false;
            // Check if the element matches the given selector criteria (TagName, Id, Classes)
            bool matchesTagName = string.IsNullOrEmpty(selector.TagName) || element.Name == selector.TagName;
            bool matchesId = string.IsNullOrEmpty(selector.Id) || element.Id == selector.Id;
            if(element.Classes!=null)
            matchesClasses = selector.Classes.Any(c => element.Classes.Contains(c));

            return matchesTagName && matchesId && matchesClasses;
        }



    }
}

using Microsoft.EntityFrameworkCore;
using NLP.Enums;
using NLP.Models;

namespace NLP.Services
{
    public class MorphoLogicalService
    {
        private readonly AppDbContext context;

        public MorphoLogicalService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Excute(string s)
        {
            var dics = await context.Words.ToListAsync();

            string[] words = Helpers.Helpers.SentenceToWords(s);
            string ans = "";
            string prefix = "";
            string sufix = "";
            foreach (var word in words)
            {
                prefix = "";
                sufix = "";
                string newWord = "";
                ans += String.Join(ans, word + " : ");
                if (dics.Where(x => x.Type == WordType.حرف_جر).Select(x => x.Text).Contains(word))
                {
                    ans += "حرف جر";
                }
                else if (dics.Where(x => x.Type == WordType.حرف_عطف).Select(x => x.Text).Contains(word))
                {
                    ans += "حرف عطف";
                }
                else if (dics.Where(x => x.Type == WordType.ضمير_منفصل).Select(x => x.Text).Contains(word))
                {
                    ans += "ضمير رفع منفصل";
                }
                else if (dics.Where(x => x.Type == WordType.اسم_جامد).Select(x => x.Text).Contains(word))
                {
                    ans += "اسم جامد";
                }
                else if (dics.Where(x => x.Type == WordType.أخرى).Select(x => x.Text).Contains(word))
                {
                    ans += "كلمة غير قابلة للاشتقاق";
                }
                else
                {
                    foreach (var pre in dics.Where(x => x.Type == WordType.سابقة).Select(x => x.Text))
                    {
                        newWord = word;
                        if (word.StartsWith(pre) && word.Length - pre.Length >= 3)
                        {
                            prefix = pre;
                            newWord = word.Remove(0, pre.Length);
                            break;
                        }
                    }

                    foreach (var suf in dics.Where(x => x.Type == WordType.لاحقة).Select(x => x.Text))
                    {
                        if (newWord.EndsWith(suf) && newWord.Length - suf.Length >= 3)
                        {
                            sufix = suf;
                            newWord = newWord.Remove(newWord.Length - suf.Length, suf.Length);
                            break;
                        }
                    }

                    if (dics.Where(x => x.Type == WordType.اسم_جامد).Select(x => x.Text).Contains(newWord))
                    {
                        ans += "اسم جامد";
                    }
                    else
                    {
                        ans += "مشتق، وزنه: " + getDerivatives(newWord, prefix, sufix);
                    }

                }
                ans += "\r\n";
            }

            return ans;
        }

        public string getDerivatives(string word, string pre, string suf)
        {
            var result = "";
            result += pre;
            if (word.Length == 3)
            {
                result += "فعل";
                result += suf;
                result += " ، جذره: " + word;
            }
            else if (word.Length == 4)
            {
                if (word[0] == 'أ')
                {
                    result += "أفعل";
                    result += suf;
                    result += " ، جذره: " + word.Remove(0,1);
                }
                else if (word[1] == 'ا')
                {
                    result += "فاعل";
                    result += suf;
                    result += " ، جذره: " + word.Remove(1,1);
                }
                else if (word[2] == 'ا')
                {
                    result += "فعال";
                    result += suf;
                    result += " ، جذره: " + word.Remove(2,1);
                }
                else if (word[2] == 'ي')
                {
                    result += "فعيل";
                    result += suf;
                    result += " ، جذره: " + word.Remove(2,1);
                }
                else
                {
                    return "جذر غير معروف";
                }
            }
            else if (word.Length == 5)
            {
                result += "مفعول";
                result += suf;
                result += " ، جذره: " + word.Remove(0,1).Remove(2,1);
            }
            else
            {
                return "جذر غير معروف";
            }
            
            return result;
        }
    }
}

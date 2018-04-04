using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileAssignment2.DataAccess;

namespace MobileAssignment2
{
    class QuizAdapter : BaseAdapter<Quiz>
    {
        Context context;
        public List<Quiz> QuizList { get; }

        public QuizAdapter(Context context, List<Quiz> quizes)
        {
            this.context = context;
            QuizList = quizes;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var quizInfoView = convertView;
            if(quizInfoView==null)
            {
                var inflator = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                quizInfoView = inflator.Inflate(Resource.Layout.QuizInfoRow, parent, false);

                var quizQuestionImage = quizInfoView.FindViewById<ImageView>(Resource.Id.imageViewQuiz);
                var quizQuestionView = quizInfoView.FindViewById<TextView>(Resource.Id.lblQuizQuestion);
                var quizAnswerView = quizInfoView.FindViewById<TextView>(Resource.Id.lblQuizAnswer);

                var qViewHolder = new QuizAdapterViewHolder(quizQuestionImage,quizQuestionView, quizAnswerView);
                quizInfoView.Tag = qViewHolder;
            }
            var cachedQuizViewHolder = quizInfoView.Tag as QuizAdapterViewHolder;
            cachedQuizViewHolder.QuizImage.SetImageResource(QuizList[position].imageQuizID);
            cachedQuizViewHolder.QuizQuestion.Text = QuizList[position].Question;
            cachedQuizViewHolder.QuizAnswer.Text = QuizList[position].RightAnswer;

            return quizInfoView;
        }

        public override int Count
        {
            get
            {
                return QuizList.Count;
            }
        }
        public override Quiz this[int position]
        {
            get
            {
                return QuizList[position];
            }
        }
    }
    class QuizAdapterViewHolder : Java.Lang.Object
    {
        public TextView QuizQuestion { get; }
        public TextView QuizAnswer { get; }
        public ImageView QuizImage { get; }

        public QuizAdapterViewHolder(ImageView quizimage,TextView question, TextView answer)
        {
            QuizImage = quizimage;
            QuizQuestion = question;
            QuizAnswer = answer;
        }
    }
}
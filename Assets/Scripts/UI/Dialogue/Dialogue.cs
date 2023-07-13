using System;
using System.Collections.Generic;
using System.IO;

namespace Dialogue
{
    class Program
    {
        static void Main(string[] args)
        {
            // CSV 파일의 경로를 지정합니다.
            string filePath = "dialogue.csv";

            // CSV 파일을 읽습니다.
            var reader = File.OpenText(filePath);

            // 대화 데이터를 저장할 리스트를 생성합니다.
            List<Dialogue> dialogues = new List<Dialogue>();

            // CSV 파일의 각 줄을 읽습니다.
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // 줄을 구분자로 분리합니다.
                string[] parts = line.Split(",");

                // 대화 ID를 가져옵니다.
                int id = int.Parse(parts[0]);

                // 화자를 가져옵니다.
                string speaker = parts[1];

                // 대화 내용을 가져옵니다.
                string text = parts[2];

                // 대화 데이터를 리스트에 추가합니다.
                dialogues.Add(new Dialogue
                {
                    Id = id,
                    Speaker = speaker,
                    Text = text
                });
            }

            // 대화 ID와 대화 내용을 맵으로 저장합니다.
            Dictionary<int, string> dialogueMap = new Dictionary<int, string>();
            foreach (var dialogue in dialogues)
            {
                dialogueMap.Add(dialogue.Id, dialogue.Text);
            }

            // 대화 ID와 대화 내용을 출력합니다.
            foreach (var pair in dialogueMap)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }
    }

    class Dialogue
    {
        public int Id { get; set; }
        public string Speaker { get; set; }
        public string Text { get; set; }
    }
}
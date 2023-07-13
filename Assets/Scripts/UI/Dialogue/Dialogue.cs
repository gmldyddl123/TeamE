using System;
using System.Collections.Generic;
using System.IO;

namespace Dialogue
{
    class Program
    {
        static void Main(string[] args)
        {
            // CSV ������ ��θ� �����մϴ�.
            string filePath = "dialogue.csv";

            // CSV ������ �н��ϴ�.
            var reader = File.OpenText(filePath);

            // ��ȭ �����͸� ������ ����Ʈ�� �����մϴ�.
            List<Dialogue> dialogues = new List<Dialogue>();

            // CSV ������ �� ���� �н��ϴ�.
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // ���� �����ڷ� �и��մϴ�.
                string[] parts = line.Split(",");

                // ��ȭ ID�� �����ɴϴ�.
                int id = int.Parse(parts[0]);

                // ȭ�ڸ� �����ɴϴ�.
                string speaker = parts[1];

                // ��ȭ ������ �����ɴϴ�.
                string text = parts[2];

                // ��ȭ �����͸� ����Ʈ�� �߰��մϴ�.
                dialogues.Add(new Dialogue
                {
                    Id = id,
                    Speaker = speaker,
                    Text = text
                });
            }

            // ��ȭ ID�� ��ȭ ������ ������ �����մϴ�.
            Dictionary<int, string> dialogueMap = new Dictionary<int, string>();
            foreach (var dialogue in dialogues)
            {
                dialogueMap.Add(dialogue.Id, dialogue.Text);
            }

            // ��ȭ ID�� ��ȭ ������ ����մϴ�.
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
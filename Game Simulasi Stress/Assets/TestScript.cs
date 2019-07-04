using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static string level;

    // kelas sederhana ini di buat untuk memepermudah pencocokan nama dan score
    // saat melakukan sorting
    [System.Serializable]
    public class HighScore
    {
        // variabel untuk mentimpan pasangan nama dan scorenya
        public string namaPlayer;
        public int score;

        // constructor untuk instansiasi
        public HighScore(string namaPlayer, int score) {
            this.namaPlayer = namaPlayer;
            this.score = score;
        }
    }

    // fungsi untuk menyimpan highscore
    public static  void simpanHighscore()
    {
        // jumlah maksimal highscore yang dapat ditampilkan
        int max = 3;

        // buat list class highscore untuk mempermudah sorting/mengurutkan
        List<HighScore> highScores = new List<HighScore>();

        // masukkan highscore yang sudah ada ke dalam list
        for (int i = 1; i <= max; i++) {
            highScores.Add (new HighScore(
                PlayerPrefs.GetString(level + "player" + i), 
                PlayerPrefs.GetInt(level + i)) 
                );
        }

        // masukkan score baru ke dalam list yang sama
        highScores.Add(new HighScore(
            ScoreTextScript.namaPlayer, 
            ScoreTextScript.coinAmount)
            );

        // urutkan list sesuai dengan score tertinggi
        highScores.Sort((p1,p2)=>p1.score.CompareTo(p2.score));
        highScores.Reverse();

        // simpan score yang sudah di urutkan sampai dengan
        // batas maksimal score yang dapat ditampilkan
        for (int i = 1; i <= max; i++)
        {
            PlayerPrefs.SetInt(level + i, highScores[i].score);
            PlayerPrefs.SetString(level + "player" + i, highScores[i].namaPlayer);
        }
    }
}

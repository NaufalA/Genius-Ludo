using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InGame;

public struct Question
{
	public int questionId;
	public string question;
	public Answer[] answers;
	public int keyAnswer;
}

public struct Answer
{
	public int answerId;
	public string answer;
}

public class Quiz : MonoBehaviour
{
	public List<Question> Questionlist = new List<Question>()
	{
		new Question {
			question="Pusat Keuangan Amerika Serikat berada di...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="New York"},
	new Answer {answerId=1, answer="Washington DC"},
	new Answer {answerId=2, answer="Chicago"},
	new Answer {answerId=3, answer="Las Vegas"},
	new Answer {answerId=4, answer="Los Angeles"},
			},
			keyAnswer=0
		},
		new Question {
			question="Berikut ini adalah negara yang dilalui oleh Pegunungan Alpen, kecuali...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Austria"},
	new Answer {answerId=1, answer="Liechtenstein"},
	new Answer {answerId=2, answer="Swiss"},
	new Answer {answerId=3, answer="Hungaria"},
	new Answer {answerId=4, answer="Slovenia"},
			},
			keyAnswer=3
		},
		new Question {
			question="Benua biru adalah sebutan bagi benua...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Asia"},
	new Answer {answerId=1, answer="Eropa"},
	new Answer {answerId=2, answer="Afrika"},
	new Answer {answerId=3, answer="Amerika"},
	new Answer {answerId=4, answer="Australia"},
			},
			keyAnswer=1
		},
		new Question {
			question="Ankara adalah ibukota dari negara……",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Libanon"},
	new Answer {answerId=1, answer="Bahrain"},
	new Answer {answerId=2, answer="Turki"},
	new Answer {answerId=3, answer="Qatar"},
	new Answer {answerId=4, answer="Suriah"},
			},
			keyAnswer=2
		},
		new Question {
			question="Allah Akbar adalah lagu kebangsaan dari negara...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Libya"},
	new Answer {answerId=1, answer="Saudi Arabia"},
	new Answer {answerId=2, answer="Afghanistan"},
	new Answer {answerId=3, answer="Suriah"},
	new Answer {answerId=4, answer="Libanon"},
			},
			keyAnswer=3
		},
		new Question {
			question="Hari perdamaian dunia diperingati setiap tanggal...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="1 Januari"},
	new Answer {answerId=1, answer="1 Februari"},
	new Answer {answerId=2, answer="1 Maret"},
	new Answer {answerId=3, answer="1 April"},
	new Answer {answerId=4, answer="1 Mei"},
			},
			keyAnswer=0
		},
		new Question {
			question="Kereta api ditemukan oleh...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Civrac"},
	new Answer {answerId=1, answer="Murdocks"},
	new Answer {answerId=2, answer="Robert Fulton"},
	new Answer {answerId=3, answer="Nikola Tesla"},
	new Answer {answerId=4, answer="Benyamin Holt"},
			},
			keyAnswer=1
		},
		new Question {
			question="Berikut ini adalah pelopor berdirinya ASEAN yang berasal dari negara Filipina, yaitu...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Adam Malik"},
	new Answer {answerId=1, answer="Tun Abdul Razak"},
	new Answer {answerId=2, answer="Thanat Khoman"},
	new Answer {answerId=3, answer="Narcisco Ramos"},
	new Answer {answerId=4, answer=" S.Rajaratnam"},
			},
			keyAnswer=3
		},
		new Question {
			question="Benua kuning adalah sebutan bagi benua...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Asia"},
	new Answer {answerId=1, answer="Eropa"},
	new Answer {answerId=2, answer="Afrika"},
	new Answer {answerId=3, answer="Amerika"},
	new Answer {answerId=4, answer="Australia"},
			},
			keyAnswer=0
		},
		new Question {
			question="Kejuaraan sepak bola dunia ( World Cup ) dilaksanakan pertama kali pada tahun 1930 di Uruguay dan juaranya adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Italia"},
	new Answer {answerId=1, answer="Inggris"},
	new Answer {answerId=2, answer="Uruguay"},
	new Answer {answerId=3, answer="Argentina"},
	new Answer {answerId=4, answer="Prancis"},
			},
			keyAnswer=2
		},
		new Question {
			question="Gubernur provinsi Jawa Timur yang pertama adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Dr. Moedjani"},
	new Answer {answerId=1, answer="R. Samadikun"},
	new Answer {answerId=2, answer="R.T. Soeryo"},
	new Answer {answerId=3, answer="Imam Utomo"},
	new Answer {answerId=4, answer="Basofi Sudirman"},
			},
			keyAnswer=2
		},
		new Question {
			question="Suku Bugis berada di provinsi...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Sumatera Utara"},
	new Answer {answerId=1, answer="Sulawesi Selatan"},
	new Answer {answerId=2, answer="Kalimantan Timur"},
	new Answer {answerId=3, answer="NTT"},
	new Answer {answerId=4, answer="Maluku"},
			},
			keyAnswer=1
		},
		new Question {
			question="O Ina Ni Keke adalah lagu daerah yang berasal dari...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Kalimantan Selatan"},
	new Answer {answerId=1, answer="Sumatera Barat"},
	new Answer {answerId=2, answer="Sulawesi Utara"},
	new Answer {answerId=3, answer="Maluku"},
	new Answer {answerId=4, answer="NTB"},
			},
			keyAnswer=2
		},
		new Question {
			question="Tanggal 14 Agustus diperingata sebagai hari...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Sumpah Pemuda"},
	new Answer {answerId=1, answer="Kesaktian Pancasila"},
	new Answer {answerId=2, answer="Pramuka"},
	new Answer {answerId=3, answer="PMI"},
	new Answer {answerId=4, answer="Guru"},
			},
			keyAnswer=2
		},
		new Question {
			question="Taman hutan raya Ir. Juanda terletak di provinsi...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Jawa Barat"},
	new Answer {answerId=1, answer="Jawa Tengah"},
	new Answer {answerId=2, answer="Jawa Timur"},
	new Answer {answerId=3, answer="DKI Jakarta"},
	new Answer {answerId=4, answer="Banten"},
			},
			keyAnswer=0
		},
		new Question {
			question="Pasukan perdamaian Indonesia dibawah bendera PBB yang diberi nama Pasukan Garuda I (yang pertama) dikirim ke wilayah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Kongo"},
	new Answer {answerId=1, answer="Vietnam"},
	new Answer {answerId=2, answer="Mesir"},
	new Answer {answerId=3, answer="Iraq"},
	new Answer {answerId=4, answer="Palestina"},
			},
			keyAnswer=2
		},
		new Question {
			question="Kabinet yang dipimpin oleh Presiden Megawati Soekarnoputri dinamakan...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Reformasi Pembangunan"},
	new Answer {answerId=1, answer="Indonesia Bersatu"},
	new Answer {answerId=2, answer="Gotong Royong"},
	new Answer {answerId=3, answer="Persatuan Nasional"},
	new Answer {answerId=4, answer="Indonesia Merdeka"},
			},
			keyAnswer=2
		},
		new Question {
			question="Rudi Hartono memenangi kejuaraan All England sebanyak...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="8 kali"},
	new Answer {answerId=1, answer="9 kali"},
	new Answer {answerId=2, answer="10 kali"},
	new Answer {answerId=3, answer="11 kali"},
	new Answer {answerId=4, answer="12 kali"},
			},
			keyAnswer=0
		},
		new Question {
			question="Danau yang terdalam di dunia dan terletak di Siberia yaitu...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Tanganyika"},
	new Answer {answerId=1, answer="Baikal"},
	new Answer {answerId=2, answer="Malawi"},
	new Answer {answerId=3, answer="Superior"},
	new Answer {answerId=4, answer="Titicaca"},
			},
			keyAnswer=1
		},
		new Question {
			question="Bunga nasional dari Negara Indonesia adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Anggrek"},
	new Answer {answerId=1, answer="Tulip"},
	new Answer {answerId=2, answer="Melati"},
	new Answer {answerId=3, answer="Teratai Biru"},
	new Answer {answerId=4, answer="Lily"},
			},
			keyAnswer=2
		},
		new Question {
			question="Salah satu peninggalan sejarah adalah kitab Ramayana yang dikarang oleh...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Empu Walmiki"},
	new Answer {answerId=1, answer="Empu Kanwa"},
	new Answer {answerId=2, answer="Empu Darmaja"},
	new Answer {answerId=3, answer="Empu Sedah"},
	new Answer {answerId=4, answer="Empu Prapanca"},
			},
			keyAnswer=0
		},
		new Question {
			question="Museum Satria Mandala terletak di...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Surabaya"},
	new Answer {answerId=1, answer="Bandung"},
	new Answer {answerId=2, answer="Semarang"},
	new Answer {answerId=3, answer="Jakarta"},
	new Answer {answerId=4, answer="Yogyakarta"},
			},
			keyAnswer=3
		},
		new Question {
			question="Nama kantor berita dari negara Amerika Serikat adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="ANTARA"},
	new Answer {answerId=1, answer="ANP"},
	new Answer {answerId=2, answer="AN"},
	new Answer {answerId=3, answer="ANSA"},
	new Answer {answerId=4, answer="AP"},
			},
			keyAnswer=4
		},
		new Question {
			question="Harbour Bridge terletak di Negara...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Inggris"},
	new Answer {answerId=1, answer="Italia"},
	new Answer {answerId=2, answer="Australia"},
	new Answer {answerId=3, answer="Austria"},
	new Answer {answerId=4, answer="Prancis"},
			},
			keyAnswer=2
		},
		new Question {
			question="Berikut ini adalah nama negara-negara ASEAN yang berbentuk kerajaan, kecuali...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Thailand"},
	new Answer {answerId=1, answer="Malaysia"},
	new Answer {answerId=2, answer="Filipina"},
	new Answer {answerId=3, answer="Brunei Darussalam"},
	new Answer {answerId=4, answer="Kamboja"},
			},
			keyAnswer=2
		},
		new Question {
			question="Yang paling baik digunakan untuk membersihkan luka akibat bahan kimia adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Tisu"},
	new Answer {answerId=1, answer="Sabun"},
	new Answer {answerId=2, answer="Air mengalir"},
	new Answer {answerId=3, answer="Alkohol"},
	new Answer {answerId=4, answer="Lap kain"},
			},
			keyAnswer=2
		},
		new Question {
			question="Ibukota negara Guatemala adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Guatemala"},
	new Answer {answerId=1, answer="Bridgetown"},
	new Answer {answerId=2, answer="Managua"},
	new Answer {answerId=3, answer="Havana"},
	new Answer {answerId=4, answer="Lome"},
			},
			keyAnswer=0
		},
		new Question {
			question="Berikut ini yang artinya tutup/keluar kecuali...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Quit"},
	new Answer {answerId=1, answer="Close"},
	new Answer {answerId=2, answer="Exit"},
	new Answer {answerId=3, answer="Quitter"},
	new Answer {answerId=4, answer="Stop"},
			},
			keyAnswer=4
		},
		new Question {
			question="Yang bukan merupakan film animasi dari perusahaan DreamWorks adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Megamind"},
	new Answer {answerId=1, answer="Inside Out"},
	new Answer {answerId=2, answer="Kung Fu Panda"},
	new Answer {answerId=3, answer="Madagascar"},
	new Answer {answerId=4, answer="How to Train Your Dragon"},
			},
			keyAnswer=1
		},
		new Question {
			question="Mantan anggota Black Organization dalam anime Detective Conan adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Gin"},
	new Answer {answerId=1, answer="Bourbon"},
	new Answer {answerId=2, answer="Rye"},
	new Answer {answerId=3, answer="Rum"},
	new Answer {answerId=4, answer="Korn"},
			},
			keyAnswer=2
		},
		new Question {
			question="Contoh Software yang fungsi utamanya sama dengan Windows Media Player adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="MP3tag"},
	new Answer {answerId=1, answer="MiniLyrics"},
	new Answer {answerId=2, answer="Picasa"},
	new Answer {answerId=3, answer="Fraps"},
	new Answer {answerId=4, answer="VLC"},
			},
			keyAnswer=4
		},
		new Question {
			question="Yang setara dengan 24 rim adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="100 lusin"},
	new Answer {answerId=1, answer="600 kodi"},
	new Answer {answerId=2, answer="2000 lusin"},
	new Answer {answerId=3, answer="3000 kodi"},
	new Answer {answerId=4, answer="6000 buah"},
			},
			keyAnswer=1
		},
		new Question {
			question="Simbol garis-garis yang dibawahnya ada angka-angka yang selalu ditemukan pada produk disebut...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Codec"},
	new Answer {answerId=1, answer="Barcode"},
	new Answer {answerId=2, answer="Fingerprint"},
	new Answer {answerId=3, answer="Komposisi"},
	new Answer {answerId=4, answer="Captcha"},
			},
			keyAnswer=1
		},
		new Question {
			question="Novel Harry Potter yang pertama terbit pada tahun...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="1996"},
	new Answer {answerId=1, answer="1997"},
	new Answer {answerId=2, answer="1998"},
	new Answer {answerId=3, answer="1999"},
	new Answer {answerId=4, answer="2001"},
			},
			keyAnswer=1
		},
		new Question {
			question="Angka 5 pada keyboard standar memiliki alternatif (shift) simbol...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="@"},
	new Answer {answerId=1, answer="*"},
	new Answer {answerId=2, answer="^"},
	new Answer {answerId=3, answer="$"},
	new Answer {answerId=4, answer="%"},
			},
			keyAnswer=4
		},
		new Question {
			question="Perubahan benda padat menjadi gas disebut...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Menyublim"},
	new Answer {answerId=1, answer="Menguap"},
	new Answer {answerId=2, answer="Mengkristal"},
	new Answer {answerId=3, answer="Mencair"},
	new Answer {answerId=4, answer="Mengembun"},
			},
			keyAnswer=0
		},
		new Question {
			question="Alat pernapasan tumbuhan di batang adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Stomata"},
	new Answer {answerId=1, answer="Klorofil"},
	new Answer {answerId=2, answer="Kambium"},
	new Answer {answerId=3, answer="Lentisel"},
	new Answer {answerId=4, answer="Floem"},
			},
			keyAnswer=3
		},
		new Question {
			question="Tokoh utama film \"Despicable Me\" adalah...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Minions"},
	new Answer {answerId=1, answer="Scarlet Overkill"},
	new Answer {answerId=2, answer="Cora"},
	new Answer {answerId=3, answer="Cora"},
	new Answer {answerId=4, answer="Gru"},
			},
			keyAnswer=4
		},
		new Question {
			question="Pasal 1 UUD 1945 amandemen berisi sebanyak...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="5 ayat"},
	new Answer {answerId=1, answer="4 ayat"},
	new Answer {answerId=2, answer="3 ayat"},
	new Answer {answerId=3, answer="2 ayat"},
	new Answer {answerId=4, answer="1 ayat"},
			},
			keyAnswer=2
		},
		new Question {
			question="Angka 123321 merupakan contoh bilangan...",
			answers= new Answer[]
			{
	new Answer {answerId=0, answer="Avogadro"},
	new Answer {answerId=1, answer="Kuadrat"},
	new Answer {answerId=2, answer="Prima"},
	new Answer {answerId=3, answer="Palindrom"},
	new Answer {answerId=4, answer="Imajiner"},
			},
			keyAnswer=3
		},
	};

	public static GameObject Instance;
	public GameObject questionDisplay;
	public AudioSource[] audio = new AudioSource[3];
	public GameObject[] answerDisplay = new GameObject[5];
	Text questionDisplayText;
	Text[] answerDisplayText = new Text[5];
	public Question currentQuestion;
	public int playerAnswer;
	public bool isCorrect;
	public bool takingQuiz;

	private void Awake()
	{
		Instance = this.gameObject;
		Instance.SetActive(false);

		questionDisplayText = questionDisplay.GetComponent<Text>();
		for (int i = 0; i < answerDisplayText.Length; i++)
		{
			answerDisplayText[i] = answerDisplay[i].GetComponent<Text>();
		}
	}

	public void StartQuiz()
	{
		Instance.SetActive(true);
		audio[2].Play();
		StartCoroutine(SetupQuiz());
	}

	public IEnumerator SetupQuiz()
	{
		GameManager.Instance.gameData.turnPossible = false;
		takingQuiz = true;

		int randomQuestion = Random.Range(0, Questionlist.Count);
		currentQuestion = Questionlist[randomQuestion];
		playerAnswer = -1;
		isCorrect = false;

		questionDisplayText.text = currentQuestion.question;
		for (int i = 0; i < currentQuestion.answers.Length; i++)
		{
			answerDisplayText[i].text = currentQuestion.answers[i].answer;
		}

		while (playerAnswer == -1)
		{
			yield return null;
		}

		Debug.Log(isCorrect);
		yield return new WaitForSeconds(1f);
		GameManager.Instance.gameData.turnPossible = true;
		takingQuiz = false;
		Instance.SetActive(false);
	}

	public void SelectAnswer(int answerId)
	{
		playerAnswer = answerId;

		if (playerAnswer == currentQuestion.keyAnswer)
		{
			isCorrect = true;
			audio[0].Play();
		}
		else
		{
			isCorrect = false;
			audio[1].Play();
		}
	}
}
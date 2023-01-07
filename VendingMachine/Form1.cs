using System.Windows.Forms.VisualStyles;

namespace VendingMachine {
	public partial class Form1 : Form {
		int countJuice = 0;
		int countSoda = 0;
		int countAlcohol = 0;
		List<Drink> arr = new List<Drink>();
		
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			try {
				arr.Add(new Juice(float.Parse(textBox1.Text == "" ? "1" : textBox1.Text), comboBox1.Text, checkBox1.Checked));
				countJuice++;
				label11.Text = countJuice.ToString();
			} catch (FormatException) {
				return;
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			try {
				arr.Add(new Soda(float.Parse(textBox11.Text == "" ? "1" : textBox11.Text), comboBox2.Text, int.Parse(textBox33.Text == "" ? "0" : textBox33.Text)));
				countSoda++;
				label12.Text = countSoda.ToString();
			} catch (FormatException) {
				return;
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			try {
				arr.Add(new Alcohol(float.Parse(textBox111.Text == "" ? "1" : textBox111.Text), comboBox3.Text, comboBox4.Text));
				countAlcohol++;
				label13.Text = countSoda.ToString();
			} catch (FormatException) {
				return;
			}
		}

		private void button4_Click(object sender, EventArgs e) {
			if (arr.Count > 0) {
				if (panel1.BackgroundImage != null) {
					panel1.BackgroundImage.Dispose();
				}
				int randInd = (new Random()).Next(0, arr.Count - 1);
				richTextBox1.Text = arr[randInd].Print();
				panel1.BackgroundImage = new Bitmap(Environment.CurrentDirectory + @$"\src\{arr[randInd].info}.png");

				switch (arr[randInd].info) {
					case "Сок":
						countJuice--;
						label11.Text = countJuice.ToString();
						break;
					case "Газировка":
						countSoda--;
						label12.Text = countSoda.ToString();
						break;
					case "Алкоголь":
						countAlcohol--;
						label13.Text = countAlcohol.ToString();
						break;
				}

				arr.RemoveAt(randInd);
			} else {
				richTextBox1.Text = "Автомат пуст";
				if (panel1.BackgroundImage != null) {
					panel1.BackgroundImage.Dispose();
				}
				panel1.BackgroundImage = null;
			}
		}
    }

    public class Drink {
		public string info;
		public float V { get; }

		public Drink(float V) {
			info = "";
			this.V = V;
		}

		public virtual string Print() {
			return $"-{info}-\nОбъём: {V} л.\n";
		}
	}

	public class Juice : Drink {
		public string fruit { get; }
		public bool pulp { get; }

		public Juice(float V, string fruit, bool pulp) : base(V) {
			info = "Сок";
			this.fruit = fruit;
			this.pulp = pulp;
		}

		public override string Print() {
			return $"{base.Print()}Фрукт: {fruit}\nМякоть: {pulp}";
		}
	}

	public class Soda : Drink {
		public string type { get; }
		public int numberOfBubbles { get; }

		public Soda(float V, string type, int numberOfBubbles) : base(V) {
			info = "Газировка";
			this.type = type;
			this.numberOfBubbles = numberOfBubbles;
		}

		public override string Print() {
			return $"{base.Print()}Вид: {type}\nКол-во Пузырьков: {numberOfBubbles}";
		}
	}

	public class Alcohol : Drink {
		public string strength { get; }
		public string type { get; }

		public Alcohol(float V, string type, string strength) : base(V) {
			info = "Алкоголь";
			this.type = type;
			this.strength = strength;
		}

		public override string Print() {
			return $"{base.Print()}Крепость: {strength}\nТип: {type}";
		}
	}
}
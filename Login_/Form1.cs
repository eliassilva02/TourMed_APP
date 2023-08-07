using Npgsql;
using System.Data;
using System.Text.RegularExpressions;

namespace Login_
{
    public partial class Form1 : Form
    {
        bool oi = true;
        private NpgsqlConnection conn = new NpgsqlConnection(connectionString: "Server=localhost; Port=5432; User Id=postgres; Password=BateraDeus;");

        public Form1()
        {
            InitializeComponent();
        }

        private bool check_User(int login, string password)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString:"Server=localhost; Port=5432; User Id=postgres; Password=BateraDeus; Database=Anvisa;");
            conn.Open(); 

            NpgsqlCommand comando = new NpgsqlCommand();
            comando.Connection = conn;

            comando.CommandType = CommandType.Text;

            comando.CommandText = "SELECT id, password " +
                                  "FROM users" +
                                  $" WHERE id = {login}" +
                                  $" AND password = '{password}'";
            NpgsqlDataReader reader = comando.ExecuteReader();
            bool response = reader.HasRows;

            conn.Close();

            return response;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (oi)
            {
                txtSenha.PasswordChar = '\0';
                oi = !oi;
            }
            else
            {
                txtSenha.PasswordChar = '*';
                oi = true;
            }
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int login = int.Parse(txtLogin.Text);
            string senha = txtSenha.Text;

            bool user = Regex.IsMatch(txtLogin.Text, "^([0-9]{4})$");
            bool passw = Regex.IsMatch(txtSenha.Text, "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*:;]).{8,}$");

            if (!user || !passw)
                MessageBox.Show("Email ou senha inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
                if (check_User(login, senha))
                    MessageBox.Show("Login feito com sucesso!!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Email ou senha inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}
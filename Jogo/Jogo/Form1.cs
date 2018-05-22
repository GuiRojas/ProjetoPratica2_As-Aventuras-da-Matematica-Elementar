using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using Jogo;

namespace login
{
    public partial class frmLogin : Form
    {

        string cs = Properties.Settings.Default.BDPRII17182ConnectionString;

        public frmLogin()
        {
            InitializeComponent();
            emailTextBox.Focus();
        }

        private void usuarioBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //devidas precauções
            if (emailTextBox.Text.Equals("") || senhaTextBox.Text.Equals("") || nomeTextBox.Text.Equals(""))
            {
                MessageBox.Show("Preencha Todos os Campos");
                return;
            }

            if (!IsValidEmail(this.emailTextBox.Text))
            {
                MessageBox.Show("Email Inválido");
                emailTextBox.Focus();
                return;
            }
            
            //o progresso deve sempre ser iniciado como 1
            progressoLabel1.Text = "1";

            String senha = senhaTextBox.Text;
            senhaTextBox.Text = senhaTextBox.Text.GetHashCode().ToString();
            //efetua devida inclusão
            this.Validate();
            this.usuarioBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bDPRII17182DataSet);

            senhaTextBox.Text = senha;
            //como os métodos de banco de dados são usados através de textbox,
            //é criada uma variável auxiliar para que a senha seja criptografada
            //sem que o usuário veja
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'bDPRII17182DataSet.usuario'. Você pode movê-la ou removê-la conforme necessário.

            //this.usuarioTableAdapter.Fill(this.bDPRII17182DataSet.usuario);
            //não quero que preencha automaticamente o formulário

            emailTextBox.Focus();
            nomeTextBox.Visible = false;
            //nome não é usado no login
            nomeLabel.Visible = false;
            progressoLabel1.Visible = false;
            //progresso não deve ser visível, dado que o valor é pardrão
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //devidas precauções
            if (emailTextBox.Text.Equals("") || senhaTextBox.Text.Equals(""))
            {
                MessageBox.Show("Preencha Todos os Campos");
                return;
            }         

            if (!IsValidEmail(this.emailTextBox.Text))
            {
                MessageBox.Show("Email Inválido");
                emailTextBox.Focus();
                return;
            }

            //Faz o login
            try
            {

                SqlConnection con = new SqlConnection();
                cs = cs.Substring(cs.IndexOf("Data Source"));
                con.ConnectionString = cs;

                string cmd_s = "SELECT * FROM usuario WHERE email = '" + emailTextBox.Text + "'" ;
                SqlCommand cmd = new SqlCommand(cmd_s, con);

                con.Open();

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);

                con.Close();

                if (ds.Tables[0].Rows.Count >= 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (senhaTextBox.Text.GetHashCode().ToString()==dr["senha"].ToString())
                    {
                        frmJogo frm = new frmJogo(progressoLabel1.Text);
                        frm.Show();
                        frm.Visible = true;
                        this.Visible = false;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Senha Incorreta!");
                        senhaTextBox.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Email Não Cadastrado!");
                    emailTextBox.Focus();
                    return;
                }

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        //verifica validade do email
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void emailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                e.Handled = true;
                senhaTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                e.Handled = true;
                btnLogin.Focus();
            }
        }

        private void senhaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                e.Handled = true;
                nomeTextBox.Focus();
            }
        }

        private void lblNome_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //botão que exibe formulário de cadastro
            bindingNavigatorAddNewItem.PerformClick();
            emailTextBox.Focus();
            senhaTextBox.Text = "";
            nomeTextBox.Visible = true;
            nomeLabel.Visible = true;
            btnLogin.Enabled = false;
            btnFormCadastro.Enabled = false;
            label1.Text = "Cadastro";
            btnRetorno.Visible = true;
            btnCadastro.Visible = true;
            progressoLabel1.Text = "1";
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void usuarioBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //muda para formulário de login
            nomeTextBox.Visible = false;
            nomeLabel.Visible = false;
            btnLogin.Enabled = true;
            btnFormCadastro.Enabled = true;
            label1.Text = "Login";
            btnRetorno.Visible = false;
            btnCadastro.Visible = false;
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            //botão da fonte de dados lida com toda a inclusão
            usuarioBindingNavigatorSaveItem.PerformClick();            
        }

        //knuth hash -- usada para criptografa senhas que serão guardadas no banco
        static int CalculateHash(String read)
        {
            int hash = 13*67*91;

            int hashedValue = hash;
            for(int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= hash;
            }
            return hashedValue;
        }
    }
}

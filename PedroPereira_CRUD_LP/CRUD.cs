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

namespace PedroPereira_CRUD_LP
{
    public partial class CRUD : Form
    {
        static string strCn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\henry\\source\\repos\\PedroPereira_CRUD_LP\\PedroPereira_CRUD_LP\\DBLP.mdf;Integrated Security=True";
        SqlConnection conexao = new SqlConnection(strCn);
        public CRUD()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string pesquisa = "select * from linguagens where codigo = " + txtCodigo.Text;
            SqlCommand cmd = new SqlCommand(pesquisa, conexao);
            SqlDataReader DR;
            try
            {
                conexao.Open();
                DR = cmd.ExecuteReader();
                if (DR.Read())
                {
                    txtCodigo.Text = DR.GetValue(0).ToString();
                    txtLinguagem.Text = DR.GetValue(1).ToString();
                    txtDesc.Text = DR.GetValue(2).ToString();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado");
                    txtLinguagem.Clear();
                    txtDesc.Clear();
                    txtCodigo.Focus();
                }
                DR.Close();
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            string adiciona = "insert into linguagens values (" +
                txtCodigo.Text + ",'" +
                txtLinguagem.Text + "','" +
                txtDesc.Text + "')";
            SqlCommand cmd = new SqlCommand(adiciona, conexao);
            try
            {
                conexao.Open();
                int resultado;
                resultado = cmd.ExecuteNonQuery();
                if (resultado == 1)
                {
                    MessageBox.Show("Registro adicionado com sucesso");
                    txtCodigo.Clear();
                    txtLinguagem.Clear();
                    txtDesc.Clear();
                    txtCodigo.Focus();
                }
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string altera = "update linguagens set linguagem= '" + txtLinguagem.Text +
                "', descricao= '" + txtDesc.Text +
                "'where codigo= " + txtCodigo.Text;
            SqlCommand cmd = new SqlCommand(altera, conexao);
            try
            {
                conexao.Open();
                int resultado;
                resultado = cmd.ExecuteNonQuery();
                if (resultado == 1)
                {
                    txtCodigo.Clear();
                    txtLinguagem.Clear();
                    txtDesc.Clear();
                    txtCodigo.Focus();
                    MessageBox.Show("Registro alterado com sucesso");
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            string remove = "delete from linguagens where codigo= " + txtCodigo.Text;
            SqlCommand cmd = new SqlCommand(remove, conexao);
            try
            {
                conexao.Open();
                int resultado;
                if(MessageBox.Show("Tem certeza que deseja remover este registro?",
                    "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    resultado = cmd.ExecuteNonQuery();
                    if (resultado == 1)
                    {
                        txtCodigo.Clear();
                        txtLinguagem.Clear();
                        txtDesc.Clear();
                        txtCodigo.Focus();
                        MessageBox.Show("Registro removido com sucesso");
                    }
                    cmd.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtLinguagem.Clear();
            txtDesc.Clear();
            txtCodigo.Focus();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home tela = new Home();
            tela.ShowDialog();
            this.Close();
        }
    }
}

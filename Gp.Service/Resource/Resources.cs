using System;
using System.Collections.Generic;

namespace Gp.Service.Resources
{
    public static class Resource
    {
        #region propriedades
        public const string Nome = "nome";
        public const string Ativo = "ativo";
        public const string Intencao = "intenção";
        public const string IntencaoDeMissa = "intenção de missa";
        public const string DataInicio = "data inicio";
        public const string DataTermino = "data termino";
        public const string DataDia = "data dia";
        public const string Hora = "hora";
        public const string TipoIntencao = "tipo intenção";
        public const string IdTipoIntencao = "id tipo intenção";
        public const string Ordem = "ordem";
        public const string LinhasEmBranco = "linhas em branco";
        public const string Ocorrencia = "Ocorrência do Mês";
        public const string SelecaoDeDias = "Seleção do Dia";
        public const string DiaDeterminado = "Dia Determinado";

        #endregion

        #region Mensagens
        public const string MSG_OperacaoComErro = "Operação não foi realizada com sucesso.";
        public const string MSG_Campo_Obrigatorio = @"O campo {0} é obrigatório!";
        public const string MSG_Lengh_Campo = @"O campo {0} deve conter entre {1} e {2} caracteres.";
        public const string MSG_Max_Lengh_Campo = @"O campo {0} deve conter no máximo {1} caracteres.";
        public const string MSG_Min_Lengh_Campo = @"O campo {0} deve conter no mínimo {1} caracteres.";
        public const string MSG_Campo_Invalido = @"O campo {0} é inválido!";
        public const string MSG_Periodo_Preparacao_Invalido = "O período de preparação é inválido!";
        public const string MSG_Campo_Maior_Zero = @"O campo {0} deve ser maior que zero!";
        public const string MSG_Campo_Hora_Invalida = @"A {0} informada não é válida. Use o formato hh:mm.";
        public const string MSG_OperacaoRealizadaSucesso = "Operação realizada com sucesso.";
        public const string MSG_Nao_localizado = "Nao foi possível localizar {0}.";
        public const string MSG_FiltroVazio = "Verifique as propriedades do filtro";
        public const string MSG_Data_Final_Menor_Data_Inicial = "A data final não pode ser menor que a data inicial.";
        public const string MSG_Somente_O_Campo_Eh_Valido = "Somente o campo {0} é valido nesse endpoint.";
        public const string MSG_Data_Inicio_Maior_Que_Data_Fim = "A {0} não pode ser maior que a {1}.";
        public const string MSG_Data_Fim_Menor_Que_Data_Inicio = "A {0} não pode ser menor que a {1}.";
        public const string MSG_Dia_Inexistente_No_Periodo_Informado = "dia inexistente para o período informado.";
        public const string MSG_Dia_Invalido = "O campo {0} deve-se conter entre os número 1 e 31.";
        public const string MSG_Campo_Nao_Pode_Ser_Negativo = "O campo não pode conter números negativos.";
        public const string MSG_Campo_Deve_Conter_Maximo_Dois_Digitos = "O campo deve conter no máximo dois dígitos.";
        public const string MSG_Ordem_Ja_Cadastrada = "Já existe um registro com este número de Ordem.";
        public const string MSG_Usuario_Ou_Senha_Invalida = "Usuário ou senha invalido.";
        #endregion
    }
}

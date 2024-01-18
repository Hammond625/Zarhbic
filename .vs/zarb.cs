using System;
using System.Collections.Generic;

class Calculator
{
    static void Main()
    {
        string expression = EntrerDonnees(); // Service d'entrée des données
        double result = EvaluatePostfix(expression);
        AffichageResultat(result); // Service d'affichage des résultats
    }

    static string EntrerDonnees()
    {
        Console.Write("Entrez la chaîne de calcul : ");
        return Console.ReadLine();
    }

    static bool VerifOperateur(string token)
    {
        // Service de vérification des opérateurs
        switch (token)
        {
            case "+":
            case "-":
            case "*":
            case "/":
                return true;
            default:
                return false;
        }
    }

    static double Calcule(double operande1, double operande2, string operateur)
    {
        // Service de calcul
        switch (operateur)
        {
            case "+":
                return operande1 + operande2;
            case "-":
                return operande1 - operande2;
            case "*":
                return operande1 * operande2;
            case "/":
                return operande1 / operande2;
            // Ajouter d'autres opérations au besoin (multiplication, division, etc.)
            default:
                throw new ArgumentException($"Opérateur non pris en charge : {operateur}");
        }
    }

    static void AffichageResultat(double result)
    {
        // Service d'affichage des résultats
        Console.WriteLine($"Résultat : {result}");
    }

    static void GestionErreur(string message, string contexte)
    {
        // Service de gestion des erreurs
        Console.WriteLine($"Erreur : {message} ({contexte})");
        Environment.Exit(1);
    }

    static double EvaluatePostfix(string expression)
    {
        List<double> stack = new List<double>();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double operand))
            {
                // Si c'est un nombre, ajouter à la liste
                stack.Add(operand);
            }
            else if (VerifOperateur(token))
            {
                // Vérifier si suffisamment d'opérandes sont présents
                if (stack.Count < 2)
                {
                    GestionErreur("Nombre d'opérandes insuffisant", "Opération impossible");
                }

                // Si suffisamment d'opérandes, récupérer les deux derniers opérandes, effectuer l'opération, puis ajouter le résultat
                double operand2 = stack[^1];
                double operand1 = stack[^2];

                stack.RemoveAt(stack.Count - 1); // Enlever le dernier élément (operand2)
                stack.RemoveAt(stack.Count - 1); // Enlever l'avant-dernier élément (operand1)

                double result = Calcule(operand1, operand2, token);
                stack.Add(result);
            }
            else
            {
                // Gestion des erreurs pour les opérateurs non pris en charge
                GestionErreur($"Opérateur non pris en charge : {token}", "Opération impossible");
            }
        }

        // Vérifier si un seul résultat est resté sur la pile
        if (stack.Count != 1)
        {
            GestionErreur("Nombre d'opérandes restant incorrect", "Opération impossible");
        }

        // Le résultat final doit être le seul élément restant dans la liste
        return stack[0];
    }
}

using System.Linq;

public static class Captalize
{
    private static bool _deuUpper;

    public static string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();

        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }

        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            _deuUpper = false;
            char _char = ' ';
            char _prevChar = ' ';
            char _nextChar = ' ';

            string _charMaisUm = "";
            char _prevCharMaisUm = ' ';
            char _nextCharMaisUm = ' ';

            string _charMaisDois = "";
            char _prevCharMaisDois = ' ';
            char _nextCharMaisDois = ' ';

            string _charMaisTres = "";
            char _prevCharMaisTres = ' ';
            char _nextCharMaisTres = ' ';

            try
            {
                _char = array[i];
                _prevChar = array[i - 1];
                _nextChar = array[i + 1];

                _charMaisUm = array[i] + "" + _nextChar;
                _prevCharMaisUm = array[i - 2];
                _nextCharMaisUm = array[i + 2];

                _charMaisDois = array[i] + "" + _nextChar + "" + array[i + 2];
                _nextCharMaisDois = array[i + 3];

                _charMaisTres = array[i] + "" + _nextChar + "" + array[i + 2] + "" + array[i + 3];
                _nextCharMaisTres = array[i + 4];
            }
            catch
            {

            }

            //não dá upper em artigos ou l sozinho (com espaço antes e depois)
            if (new[] { 'a', 'e', 'o', 'u', 'l' }.Contains(_char) && _prevChar == ' ' && _nextChar == ' ')
                continue;

            //não dá upper nessas substrings de 2 caracters com espaço antes e depois
            if (new[] { "da", "de", "do", "du", "em", "na", "no" }.Contains(_charMaisUm) && _nextCharMaisUm == ' ' && _prevChar == ' ')
                continue;

            //não dá upper nessas substrings de 3 caracters com espaço antes e depois
            if (new[] { "das", "dos", "nas", "nos" }.Contains(_charMaisDois) && _nextCharMaisDois == ' ' && _prevChar == ' ')
                continue;

            var separadores = new[] { ' ', ',', ';', '-', '_', ':', '/', '\\', '&', '$' };

            //algarismos romanos i, v e x sozinhos dá upper
            if (!(_deuUpper) && new[] { 'i', 'v', 'x' }.Contains(_char) && separadores.Contains(_nextChar) && separadores.Contains(_prevChar))
                array[i] = UpperCaracther(array[i]);

            if (_char == 'i')
            {
                //primeiro i do ii
                if (!(_deuUpper) && (_charMaisUm == "ii" && separadores.Contains(_nextCharMaisUm) && separadores.Contains(_prevChar)))
                    array[i] = UpperCaracther(array[i]);

                //primeiro i do iii
                if (!(_deuUpper) && (_charMaisDois == "iii" && separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevCharMaisDois)))
                    array[i] = UpperCaracther(array[i]);

                //segundo i do iii
                if (!(_deuUpper) && (_nextChar == 'i' && _prevChar.ToString().ToLower() == "i" && (separadores.Contains(_prevCharMaisUm)
                    || (_prevCharMaisUm.ToString().ToLower() == "v" && separadores.Contains(_prevCharMaisDois))
                    || (_prevCharMaisUm.ToString().ToLower() == "x" && separadores.Contains(_prevCharMaisDois)))))
                    array[i] = UpperCaracther(array[i]);

                //ultimo i do iii ou ii
                if (!(_deuUpper) && (separadores.Contains(_nextChar) && _prevChar.ToString().ToLower() == "i" && (separadores.Contains(_prevCharMaisUm) || separadores.Contains(_prevCharMaisDois))))
                    array[i] = UpperCaracther(array[i]);

                //i antes do v ou x entre espacos em branco
                if (!(_deuUpper) && ((_nextChar == 'v' || _nextChar == 'x') && separadores.Contains(_prevChar) && separadores.Contains(_nextCharMaisDois)))
                    array[i] = UpperCaracther(array[i]);

                //i depois do v ou x
                if (!(_deuUpper) && ((_prevChar.ToString().ToLower() == "v" || _prevChar.ToString().ToLower() == "x") && separadores.Contains(_prevCharMaisUm)
                    && (separadores.Contains(_nextChar) || separadores.Contains(_nextCharMaisUm) || separadores.Contains(_nextCharMaisDois))))
                    array[i] = UpperCaracther(array[i]);
            }
            else if (_char == 'l')
            {
                //primeiro l do ll
                if (!(_deuUpper) && (new[] { "ll" }.Contains(_charMaisUm) && separadores.Contains(_nextCharMaisUm) && separadores.Contains(_prevChar)))
                    continue;

                //primeiro l do lll
                if (!(_deuUpper) && (new[] { "lll" }.Contains(_charMaisDois) && separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevCharMaisDois)))
                    continue;

                //segundo l do lll
                if (!(_deuUpper) && (_nextChar == 'l' && _prevChar.ToString().ToLower() == "l" && separadores.Contains(_prevCharMaisUm)))
                    continue;

                //ultimo l do lll ou ll
                if (!(_deuUpper) && (separadores.Contains(_nextChar) && _prevChar.ToString().ToLower() == "l" && (separadores.Contains(_prevCharMaisUm) || separadores.Contains(_prevCharMaisDois))))
                    continue;

                //l antes de v ou x e entre espaços
                if (!(_deuUpper) && ((_nextChar == 'v' || _nextChar == 'x') && separadores.Contains(_prevChar) && separadores.Contains(_nextCharMaisUm)))
                    continue;
            }
            if (_char == 'v' || _char == 'x')
            {
                //v ou x depois do i entre espacos em branco
                if (!(_deuUpper) && (separadores.Contains(_nextChar) && _prevChar.ToString().ToLower() == "i"))
                    array[i] = UpperCaracther(array[i]);

                //vi, vii, viii
                if (!(_deuUpper) &&
                    ((_nextChar == 'i' && separadores.Contains(_nextCharMaisUm) && separadores.Contains(_prevChar)))
                    || (_nextChar == 'i' && _nextCharMaisUm == 'i' && separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevChar))
                    || (_nextChar == 'i' && _nextCharMaisUm == 'i' && _nextCharMaisDois == 'i' && separadores.Contains(_nextCharMaisTres) && separadores.Contains(_prevChar)))
                    array[i] = UpperCaracther(array[i]);

                //v ou x depois do l e entre espacos em branco
                if (!(_deuUpper) && (_nextChar == ' ' && _prevChar.ToString().ToLower() == "l" && separadores.Contains(_prevCharMaisUm)))
                    array[i] = UpperCaracther(array[i]);

                //vl, vll, vlll
                if (!(_deuUpper) &&
                    ((_nextChar == 'l' && separadores.Contains(_nextCharMaisUm) && separadores.Contains(_prevChar)))
                    || (_nextChar == 'l' && _nextCharMaisUm == 'l' && separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevChar))
                    || (_nextChar == 'l' && _nextCharMaisUm == 'l' && _nextCharMaisDois == 'l' && separadores.Contains(_nextCharMaisTres) && separadores.Contains(_prevChar)))
                    array[i] = UpperCaracther(array[i]);

                if (_char == 'v')
                {
                    //v depois de x ou l precedido de espaço
                    if (!(_deuUpper) && ((new[] { "x", "l" }.Contains(_prevChar.ToString().ToLower()) && (separadores.Contains(_prevCharMaisUm))) || ((new[] { "x", "l" }.Contains(_prevChar.ToString().ToLower()) && (separadores.Contains(_prevCharMaisUm) || separadores.Contains(_prevCharMaisDois))))))
                        array[i] = UpperCaracther(array[i]);
                }

                if (_char == 'x')
                {
                    //x antes do iv entre espacos em branco
                    if (!(_deuUpper) && (_nextChar == 'i' && _nextCharMaisUm.ToString().ToLower() == "v" && separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevChar)))
                        array[i] = UpperCaracther(array[i]);

                    //x antes do lv entre espacos em branco
                    if (!(_deuUpper) && (_nextChar == 'l' && _nextCharMaisUm.ToString().ToLower() == "v" && separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevChar)))
                        array[i] = UpperCaracther(array[i]);

                    //x antes de v precedido de espaço
                    if (!(_deuUpper) && (separadores.Contains(_nextCharMaisDois) && separadores.Contains(_prevChar)))
                        array[i] = UpperCaracther(array[i]);
                }
            }

            if (separadores.Contains(_prevChar) && !_deuUpper)
                array[i] = UpperCaracther(array[i]);

        }
        return new string(array);
    }

    private static char UpperCaracther(char caracter)
    {
        _deuUpper = true;

        if (char.IsLower(caracter))
            return char.ToUpper(caracter);

        return ' ';
    }
}

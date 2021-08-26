namespace RestChild.Comon
{
    public enum Case
    {
        [CustomDisplayName("Именительный")] Nominative,
        [CustomDisplayName("Родительный")] Genetive,
        [CustomDisplayName("Дательный")] Dative,
        [CustomDisplayName("Творительный")] Ablative,
        [CustomDisplayName("Винительный")] Accusative,
        [CustomDisplayName("Предложный")] Prepositional
    }
}

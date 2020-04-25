using Telerik.Windows.Controls;

namespace RatingWPF
{
    class CustomLocalizationManager : LocalizationManager
    {
        public override string GetStringOverride(string key)
        {
            switch (key)
            {
                case "Close":
                    return "Закрыть";

                case "EnterDate":
                    return "Введите дату";

                case "Error":
                    return "Ошибка";

                case "Today":
                    return "Сегодня";

                case "RadDataPagerOf":
                    return "Из";

                case "RadDataPagerPage":
                    return "Страница";

                case "GridViewAlwaysVisibleNewRow":
                    return "Добавить строку";

                case "GridViewFilterIsNotContainedIn":
                    return "Не содержится в";

                case "GridViewFilterIsNotEmpty":
                    return "Не пустой";

                case "GridViewFilterIsEmpty":
                    return "Пустой";

                case "GridViewSearchPanelTopText":
                    return "полнотекстовый поиск";

                case "GridViewColumnsSelectionButtonTooltip":
                    return "Выберите колонки";


                case "GridViewFilterIsNull":
                    return "null";

                case "GridViewFilterIsNotNull":
                    return "не null";

                case "GridViewGroupPanelTopTextGrouped":
                    return "Сгруппировать по";


                case "GridViewGroupPanelTopText":
                    return "Нажмите здесь, чтобы добавить новый элемент";
                case "GridViewFilterOr":
                    return "Или";
                case "GridViewGroupPanelText":
                    return "Перетащите заголовок столбца и отпустите его здесь, чтобы сгруппировать по этому столбцу.";

                case "GridViewClearFilter":
                    return "Очистить";
                case "GridViewFilterDoesNotContain":
                    return "Не содержит";
                case "GridViewFilterShowRowsWithValueThat":
                    return "Показать строки со значением, которое:";
                case "GridViewFilterSelectAll":
                    return "Выбрать всё";
                case "GridViewFilterContains":
                    return "Содержит";
                case "GridViewFilterEndsWith":
                    return "Заканчивается на";
                case "GridViewFilterIsContainedIn":
                    return "Содержится в";
                case "GridViewFilterIsEqualTo":
                    return "Равно";
                case "GridViewFilterIsGreaterThan":
                    return "Больше чем";
                case "GridViewFilterIsGreaterThanOrEqualTo":
                    return "Больше или равно";
                case "GridViewFilterIsLessThan":
                    return "Меньше чем";
                case "GridViewFilterIsLessThanOrEqualTo":
                    return "Меньше или равно";
                case "GridViewFilterIsNotEqualTo":
                    return "Не равно";
                case "GridViewFilterStartsWith":
                    return "Начинается с";
                case "GridViewFilterAnd":
                    return "И";
                case "GridViewFilter":
                    return "Фильтр";
            }
            return base.GetStringOverride(key);
        }
    }
}

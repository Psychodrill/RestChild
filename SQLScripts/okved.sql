insert into [dbo].[Okved]
select 100 as Id, '' as Code, 'РАЗДЕЛ А СЕЛЬСКОЕ ХОЗЯЙСТВО, ОХОТА И ЛЕСНОЕ ХОЗЯЙСТВО' as Name, null ParentId, 0 as LastUpdateTick union all
select 101 as Id, '' as Code, 'РАЗДЕЛ B РЫБОЛОВСТВО, РЫБОВОДСТВО' as Name, null ParentId, 0 as LastUpdateTick union all
select 102 as Id, '' as Code, 'РАЗДЕЛ С ДОБЫЧА ПОЛЕЗНЫХ ИСКОПАЕМЫХ' as Name, null ParentId, 0 as LastUpdateTick union all
select 103 as Id, '' as Code, 'Подраздел СА ДОБЫЧА ТОПЛИВНО-ЭНЕРГЕТИЧЕСКИХ ПОЛЕЗНЫХ  ИСКОПАЕМЫХ' as Name, 102 ParentId, 0 as LastUpdateTick union all
select 104 as Id, '' as Code, 'Подраздел СВ ДОБЫЧА ПОЛЕЗНЫХ ИСКОПАЕМЫХ, КРОМЕ ТОПЛИВНО- ЭНЕРГЕТИЧЕСКИХ' as Name, 102 ParentId, 0 as LastUpdateTick union all
select 105 as Id, '' as Code, 'РАЗДЕЛ D ОБРАБАТЫВАЮЩИЕ ПРОИЗВОДСТВА' as Name, null ParentId, 0 as LastUpdateTick union all
select 106 as Id, '' as Code, 'Подраздел DA ПРОИЗВОДСТВО ПИЩЕВЫХ ПРОДУКТОВ, ВКЛЮЧАЯ НАПИТКИ, И ТАБАКА' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 107 as Id, '' as Code, 'Подраздел DB ТЕКСТИЛЬНОЕ И ШВЕЙНОЕ ПРОИЗВОДСТВО' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 108 as Id, '' as Code, 'Подраздел DC ПРОИЗВОДСТВО КОЖИ, ИЗДЕЛИЙ ИЗ КОЖИ И ПРОИЗВОДСТВО ОБУВИ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 109 as Id, '' as Code, 'Подраздел DD ОБРАБОТКА ДРЕВЕСИНЫ И ПРОИЗВОДСТВО ИЗДЕЛИЙ  ИЗ ДЕРЕВА' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 110 as Id, '' as Code, 'Подраздел DE ЦЕЛЛЮЛОЗНО-БУМАЖНОЕ ПРОИЗВОДСТВО;  ИЗДАТЕЛЬСКАЯ И ПОЛИГРАФИЧЕСКАЯ ДЕЯТЕЛЬНОСТЬ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 111 as Id, '' as Code, 'Подраздел DF ПРОИЗВОДСТВО КОКСА, НЕФТЕПРОДУКТОВ И ЯДЕРНЫХ  МАТЕРИАЛОВ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 112 as Id, '' as Code, 'Подраздел DG ХИМИЧЕСКОЕ ПРОИЗВОДСТВО' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 113 as Id, '' as Code, 'Подраздел DH ПРОИЗВОДСТВО РЕЗИНОВЫХ И ПЛАСТМАССОВЫХ ИЗДЕЛИЙ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 114 as Id, '' as Code, 'Подраздел DI ПРОИЗВОДСТВО ПРОЧИХ НЕМЕТАЛЛИЧЕСКИХ МИНЕРАЛЬНЫХ ПРОДУКТОВ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 115 as Id, '' as Code, 'Подраздел DJ МЕТАЛЛУРГИЧЕСКОЕ ПРОИЗВОДСТВО И  ПРОИЗВОДСТВО ГОТОВЫХ МЕТАЛЛИЧЕСКИХ ИЗДЕЛИЙ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 116 as Id, '' as Code, 'Подраздел DK ПРОИЗВОДСТВО МАШИН И ОБОРУДОВАНИЯ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 117 as Id, '' as Code, 'Подраздел DL ПРОИЗВОДСТВО ЭЛЕКТРООБОРУДОВАНИЯ,  ЭЛЕКТРОННОГО И ОПТИЧЕСКОГО ОБОРУДОВАНИЯ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 118 as Id, '' as Code, 'Подраздел DM ПРОИЗВОДСТВО ТРАНСПОРТНЫХ СРЕДСТВ И  ОБОРУДОВАНИЯ' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 119 as Id, '' as Code, 'Подраздел DN ПРОЧИЕ ПРОИЗВОДСТВА' as Name, 105 ParentId, 0 as LastUpdateTick union all
select 120 as Id, '' as Code, 'РАЗДЕЛ E ПРОИЗВОДСТВО И РАСПРЕДЕЛЕНИЕ ЭЛЕКТРОЭНЕРГИИ, ГАЗА И ВОДЫ' as Name, null ParentId, 0 as LastUpdateTick union all
select 121 as Id, '' as Code, 'РАЗДЕЛ F   СТРОИТЕЛЬСТВО' as Name, null ParentId, 0 as LastUpdateTick union all
select 122 as Id, '' as Code, 'РАЗДЕЛ G ОПТОВАЯ И РОЗНИЧНАЯ ТОРГОВЛЯ; РЕМОНТ АВТОТРАНСПОРТНЫХ СРЕДСТВ, МОТОЦИКЛОВ, БЫТОВЫХ ИЗДЕЛИЙ И ПРЕДМЕТОВ ЛИЧНОГО ПОЛЬЗОВАНИЯ' as Name, null ParentId, 0 as LastUpdateTick union all
select 123 as Id, '' as Code, 'РАЗДЕЛ H   ГОСТИНИЦЫ И РЕСТОРАНЫ' as Name, null ParentId, 0 as LastUpdateTick union all
select 124 as Id, '' as Code, 'РАЗДЕЛ I ТРАНСПОРТ И СВЯЗЬ' as Name, null ParentId, 0 as LastUpdateTick union all
select 125 as Id, '' as Code, 'РАЗДЕЛ J   ФИНАНСОВАЯ ДЕЯТЕЛЬНОСТЬ' as Name, null ParentId, 0 as LastUpdateTick union all
select 126 as Id, '' as Code, 'РАЗДЕЛ K ОПЕРАЦИИ С НЕДВИЖИМЫМ ИМУЩЕСТВОМ, АРЕНДА И ПРЕДОСТАВЛЕНИЕ УСЛУГ' as Name, null ParentId, 0 as LastUpdateTick union all
select 127 as Id, '' as Code, 'РАЗДЕЛ L ГОСУДАРСТВЕННОЕ УПРАВЛЕНИЕ И ОБЕСПЕЧЕНИЕ ВОЕННОЙ БЕЗОПАСНОСТИ; СОЦИАЛЬНОЕ СТРАХОВАНИЕ' as Name, null ParentId, 0 as LastUpdateTick union all
select 128 as Id, '' as Code, 'РАЗДЕЛ M   ОБРАЗОВАНИЕ' as Name, null ParentId, 0 as LastUpdateTick union all
select 129 as Id, '' as Code, 'РАЗДЕЛ N   ЗДРАВООХРАНЕНИЕ И ПРЕДОСТАВЛЕНИЕ СОЦИАЛЬНЫХ  УСЛУГ' as Name, null ParentId, 0 as LastUpdateTick union all
select 130 as Id, '' as Code, 'РАЗДЕЛ O   ПРЕДОСТАВЛЕНИЕ ПРОЧИХ КОММУНАЛЬНЫХ,  СОЦИАЛЬНЫХ И ПЕРСОНАЛЬНЫХ УСЛУГ' as Name, null ParentId, 0 as LastUpdateTick union all
select 131 as Id, '' as Code, 'РАЗДЕЛ Р ДЕЯТЕЛЬНОСТЬ ДОМАШНИХ ХОЗЯЙСТВ' as Name, null ParentId, 0 as LastUpdateTick union all
select 132 as Id, '' as Code, 'РАЗДЕЛ Q ДЕЯТЕЛЬНОСТЬ ЭКСТЕРРИТОРИАЛЬНЫХ ОРГАНИЗАЦИЙ' as Name, null ParentId, 0 as LastUpdateTick union all
select 133 as Id, '01' as Code, 'Сельское хозяйство, охота и предоставление услуг в этих областях' as Name, 2422 ParentId, 0 as LastUpdateTick union all
select 134 as Id, '01.1' as Code, 'Растениеводство' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 135 as Id, '01.11' as Code, 'Выращивание зерновых, технических и прочих сельскохозяйственных  культур, не включенных в другие группировки' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 136 as Id, '01.11.1' as Code, 'Выращивание зерновых и зернобобовых культур' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 137 as Id, '01.11.2' as Code, 'Выращивание картофеля, столовых корнеплодных и клубнеплодных культур  с высоким содержанием крахмала или инулина' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 138 as Id, '01.11.3' as Code, 'Выращивание масличных культур' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 139 as Id, '01.11.4' as Code, 'Выращивание табака и махорки' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 140 as Id, '01.11.5' as Code, 'Выращивание сахарной свеклы' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 141 as Id, '01.11.6' as Code, 'Выращивание кормовых культур; заготовка растительных кормов' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 142 as Id, '01.11.7' as Code, 'Выращивание прядильных культур' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 143 as Id, '01.11.8' as Code, 'Выращивание прочих сельскохозяйственных культур, не включенных в  другие группировки' as Name, 135 ParentId, 0 as LastUpdateTick union all
select 144 as Id, '01.12' as Code, 'Овощеводство; декоративное садоводство и производство продукции  питомников' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 145 as Id, '01.12.1' as Code, 'Овощеводство' as Name, 144 ParentId, 0 as LastUpdateTick union all
select 146 as Id, '01.12.2' as Code, 'Декоративное садоводство и производство продукции питомников' as Name, 144 ParentId, 0 as LastUpdateTick union all
select 147 as Id, '01.12.3' as Code, 'Выращивание грибов, сбор лесных грибов и трюфелей' as Name, 144 ParentId, 0 as LastUpdateTick union all
select 148 as Id, '01.12.31' as Code, 'Выращивание грибов и грибницы (мицелия)' as Name, 144 ParentId, 0 as LastUpdateTick union all
select 149 as Id, '01.12.32' as Code, 'Сбор лесных грибов и трюфелей' as Name, 144 ParentId, 0 as LastUpdateTick union all
select 150 as Id, '01.13' as Code, 'Выращивание фруктов, орехов, культур для производства напитков и  пряностей' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 151 as Id, '01.13.1' as Code, 'Выращивание винограда' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 152 as Id, '01.13.2' as Code, 'Выращивание прочих фруктов и орехов' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 153 as Id, '01.13.21' as Code, 'Выращивание плодовых и ягодных культур' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 154 as Id, '01.13.22' as Code, 'Выращивание орехов' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 155 as Id, '01.13.23' as Code, 'Выращивание посадочного материала плодовых насаждений' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 156 as Id, '01.13.24' as Code, 'Сбор плодов, ягод и орехов, в том числе дикорастущих' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 157 as Id, '01.13.3' as Code, 'Выращивание культур для производства напитков' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 158 as Id, '01.13.4' as Code, 'Выращивание культур для производства пряностей' as Name, 150 ParentId, 0 as LastUpdateTick union all
select 159 as Id, '01.2' as Code, 'Животноводство' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 160 as Id, '01.21' as Code, 'Разведение крупного рогатого скота' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 161 as Id, '01.22' as Code, 'Разведение овец, коз, лошадей, ослов, мулов и лошаков' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 162 as Id, '01.22.1' as Code, 'Разведение овец и коз' as Name, 161 ParentId, 0 as LastUpdateTick union all
select 163 as Id, '01.22.2' as Code, 'Разведение лошадей, ослов, мулов и лошаков' as Name, 161 ParentId, 0 as LastUpdateTick union all
select 164 as Id, '01.23' as Code, 'Разведение свиней' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 165 as Id, '01.24' as Code, 'Разведение сельскохозяйственной птицы' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 166 as Id, '01.25' as Code, 'Разведение прочих животных' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 167 as Id, '01.25.1' as Code, 'Разведение пчел' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 168 as Id, '01.25.2' as Code, 'Разведение кроликов и пушных зверей в условиях фермы' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 169 as Id, '01.25.3' as Code, 'Разведение шелкопряда' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 170 as Id, '01.25.4' as Code, 'Разведение оленей' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 171 as Id, '01.25.5' as Code, 'Разведение верблюдов' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 172 as Id, '01.25.6' as Code, 'Разведение домашних животных' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 173 as Id, '01.25.7' as Code, 'Разведение лабораторных животных' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 174 as Id, '01.25.8' as Code, 'Разведение водных пресмыкающихся и лягушек в водоемах, разведение  дождевых (калифорнийских) червей' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 175 as Id, '01.25.81' as Code, 'Разведение водных пресмыкающихся и лягушек в водоемах' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 176 as Id, '01.25.82' as Code, 'Разведение дождевых (калифорнийских) червей' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 177 as Id, '01.25.9' as Code, 'Разведение прочих животных, не включенных в другие группировки' as Name, 166 ParentId, 0 as LastUpdateTick union all
select 178 as Id, '01.3' as Code, 'Растениеводство в сочетании с животноводством (смешанное сельское  хозяйство)' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 179 as Id, '01.30' as Code, 'Растениеводство в сочетании с животноводством (смешанное сельское  хозяйство)' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 180 as Id, '01.4' as Code, 'Предоставление услуг в области растениеводства, декоративного садоводства и животноводства, кроме ветеринарных услуг' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 181 as Id, '01.41' as Code, 'Предоставление услуг в области растениеводства и декоративного садоводства' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 182 as Id, '01.41.1' as Code, 'Предоставление услуг, связанных с производством сельскохозяйственных культур' as Name, 181 ParentId, 0 as LastUpdateTick union all
select 183 as Id, '01.41.2' as Code, 'Предоставление услуг по закладке, обработке и содержанию садов, парков и других зеленых насаждений' as Name, 181 ParentId, 0 as LastUpdateTick union all
select 184 as Id, '01.41.3' as Code, 'Предоставление услуг по эксплуатации мелиоративных систем' as Name, 181 ParentId, 0 as LastUpdateTick union all
select 185 as Id, '01.42' as Code, 'Предоставление услуг в области животноводства, кроме ветеринарных услуг' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 186 as Id, '01.5' as Code, 'Охота и разведение диких животных, включая предоставление услуг в этих  областях' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 187 as Id, '01.50' as Code, 'Охота и разведение диких животных, включая предоставление услуг в этих  областях' as Name, 133 ParentId, 0 as LastUpdateTick union all
select 188 as Id, '02' as Code, 'Лесное хозяйство, лесозаготовки и предоставление услуг в этих областях' as Name, 2422 ParentId, 0 as LastUpdateTick union all
select 189 as Id, '02.0' as Code, 'Лесное хозяйство и предоставление услуг в этой области' as Name, 188 ParentId, 0 as LastUpdateTick union all
select 190 as Id, '02.01' as Code, 'Лесоводство и лесозаготовки' as Name, 188 ParentId, 0 as LastUpdateTick union all
select 191 as Id, '02.01.1' as Code, 'Лесозаготовки' as Name, 190 ParentId, 0 as LastUpdateTick union all
select 192 as Id, '02.01.2' as Code, 'Сбор дикорастущих и недревесных лесопродуктов' as Name, 190 ParentId, 0 as LastUpdateTick union all
select 193 as Id, '02.01.5' as Code, 'Лесоводство' as Name, 190 ParentId, 0 as LastUpdateTick union all
select 194 as Id, '02.01.6' as Code, 'Деятельность лесопитомников' as Name, 190 ParentId, 0 as LastUpdateTick union all
select 195 as Id, '02.01.61' as Code, 'Выращивание сеянцев, деревьев и кустарников' as Name, 190 ParentId, 0 as LastUpdateTick union all
select 196 as Id, '02.01.69' as Code, 'Выращивание прочей продукции питомников' as Name, 190 ParentId, 0 as LastUpdateTick union all
select 197 as Id, '02.02' as Code, 'Предоставление услуг в области лесоводства и лесозаготовок' as Name, 188 ParentId, 0 as LastUpdateTick union all
select 198 as Id, '02.02.1' as Code, 'Предоставление услуг в области лесоводства' as Name, 197 ParentId, 0 as LastUpdateTick union all
select 199 as Id, '02.02.2' as Code, 'Предоставление услуг в области лесозаготовок' as Name, 197 ParentId, 0 as LastUpdateTick union all
select 200 as Id, '05' as Code, 'Рыболовство, рыбоводство и предоставление услуг в этих областях' as Name, 2423 ParentId, 0 as LastUpdateTick union all
select 201 as Id, '05.0' as Code, 'Рыболовство, рыбоводство и предоставление услуг в этих областях' as Name, 200 ParentId, 0 as LastUpdateTick union all
select 202 as Id, '05.01' as Code, 'Рыболовство' as Name, 200 ParentId, 0 as LastUpdateTick union all
select 203 as Id, '05.01.1' as Code, 'Рыболовство в открытых районах Мирового океана и внутренних морских водах' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 204 as Id, '05.01.11' as Code, 'Вылов рыбы и водных биоресурсов в открытых районах Мирового океана и  внутренних морских водах сельскохозяйственными товаропроизводителями' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 205 as Id, '05.01.12' as Code, 'Вылов рыбы и водных биоресурсов в открытых районах Мирового океана и  внутренних морских водах несельскохозяйственными товаропроизводителями' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 206 as Id, '05.01.2' as Code, 'Рыболовство в реках, озерах, водохранилищах и прудах' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 207 as Id, '05.01.21' as Code, 'Вылов рыбы и водных биоресурсов в реках, озерах, водохранилищах и  прудах сельскохозяйственными товаропроизводителями' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 208 as Id, '05.01.22' as Code, 'Вылов рыбы и водных биоресурсов в реках, озерах, водохранилищах и  прудах несельскохозяйственными товаропроизводителями' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 209 as Id, '05.01.3' as Code, 'Предоставление услуг в области рыболовства' as Name, 202 ParentId, 0 as LastUpdateTick union all
select 210 as Id, '05.02' as Code, 'Рыбоводство' as Name, 200 ParentId, 0 as LastUpdateTick union all
select 211 as Id, '05.02.1' as Code, 'Воспроизводство рыбы и водных биоресурсов' as Name, 210 ParentId, 0 as LastUpdateTick union all
select 212 as Id, '05.02.11' as Code, 'Воспроизводство рыбы и водных биоресурсов сельскохозяйственными  товаропроизводителями' as Name, 210 ParentId, 0 as LastUpdateTick union all
select 213 as Id, '05.02.12' as Code, 'Воспроизводство рыбы и водных биоресурсов несельскохозяйственными  товаропроизводителями' as Name, 210 ParentId, 0 as LastUpdateTick union all
select 214 as Id, '05.02.2' as Code, 'Предоставление услуг, связанных с воспроизводством рыбы и водных  биоресурсов' as Name, 210 ParentId, 0 as LastUpdateTick union all
select 215 as Id, '10' as Code, 'Добыча каменного угля,бурого угля и торфа' as Name, 103 ParentId, 0 as LastUpdateTick union all
select 216 as Id, '10.1' as Code, 'Добыча, обогащение и агломерация каменного угля' as Name, 215 ParentId, 0 as LastUpdateTick union all
select 217 as Id, '10.10' as Code, 'Добыча, обогащение и агломерация каменного угля' as Name, 215 ParentId, 0 as LastUpdateTick union all
select 218 as Id, '10.10.1' as Code, 'Добыча каменного угля' as Name, 217 ParentId, 0 as LastUpdateTick union all
select 219 as Id, '10.10.11' as Code, 'Добыча каменного угля открытым способом' as Name, 217 ParentId, 0 as LastUpdateTick union all
select 220 as Id, '10.10.12' as Code, 'Добыча каменного угля подземным способом' as Name, 217 ParentId, 0 as LastUpdateTick union all
select 221 as Id, '10.10.2' as Code, 'Обогащение и агломерация каменного угля' as Name, 217 ParentId, 0 as LastUpdateTick union all
select 222 as Id, '10.10.21' as Code, 'Обогащение каменного угля' as Name, 217 ParentId, 0 as LastUpdateTick union all
select 223 as Id, '10.10.22' as Code, 'Агломерация каменного угля' as Name, 217 ParentId, 0 as LastUpdateTick union all
select 224 as Id, '10.2' as Code, 'Добыча, обогащение и агломерация бурого угля' as Name, 215 ParentId, 0 as LastUpdateTick union all
select 225 as Id, '10.20' as Code, 'Добыча, обогащение и агломерация бурого угля' as Name, 215 ParentId, 0 as LastUpdateTick union all
select 226 as Id, '10.20.1' as Code, 'Добыча бурого угля (лигнита)' as Name, 225 ParentId, 0 as LastUpdateTick union all
select 227 as Id, '10.20.11' as Code, 'Добыча бурого угля открытым способом' as Name, 225 ParentId, 0 as LastUpdateTick union all
select 228 as Id, '10.20.12' as Code, 'Добыча бурого угля подземным способом' as Name, 225 ParentId, 0 as LastUpdateTick union all
select 229 as Id, '10.20.2' as Code, 'Обогащение и агломерация бурого угля' as Name, 225 ParentId, 0 as LastUpdateTick union all
select 230 as Id, '10.20.21' as Code, 'Обогащение бурого угля' as Name, 225 ParentId, 0 as LastUpdateTick union all
select 231 as Id, '10.20.22' as Code, 'Агломерация бурого угля' as Name, 225 ParentId, 0 as LastUpdateTick union all
select 232 as Id, '10.3' as Code, 'Добыча и агломерация торфа' as Name, 215 ParentId, 0 as LastUpdateTick union all
select 233 as Id, '10.30' as Code, 'Добыча и агломерация торфа' as Name, 215 ParentId, 0 as LastUpdateTick union all
select 234 as Id, '10.30.1' as Code, 'Добыча торфа' as Name, 233 ParentId, 0 as LastUpdateTick union all
select 235 as Id, '10.30.2' as Code, 'Агломерация торфа' as Name, 233 ParentId, 0 as LastUpdateTick union all
select 236 as Id, '11' as Code, 'Добыча сырой нефти и природного газа; предоставление услуг в этих областях' as Name, 103 ParentId, 0 as LastUpdateTick union all
select 237 as Id, '11.1' as Code, 'Добыча сырой нефти и природного газа' as Name, 236 ParentId, 0 as LastUpdateTick union all
select 238 as Id, '11.10' as Code, 'Добыча сырой нефти и природного газа' as Name, 236 ParentId, 0 as LastUpdateTick union all
select 239 as Id, '11.10.1' as Code, 'Добыча сырой нефти и нефтяного (попутного) газа; извлечение фракций из  нефтяного (попутного) газа' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 240 as Id, '11.10.11' as Code, 'Добыча сырой нефти' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 241 as Id, '11.10.12' as Code, 'Добыча нефтяного (попутного) газа' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 242 as Id, '11.10.13' as Code, 'Разделение и извлечение фракций из нефтяного (попутного) газа' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 243 as Id, '11.10.2' as Code, 'Добыча природного газа и газового конденсата' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 244 as Id, '11.10.3' as Code, 'Сжижение и регазификация природного газа для транспортирования' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 245 as Id, '11.2' as Code, 'Предоставление услуг по добыче нефти и газа' as Name, 236 ParentId, 0 as LastUpdateTick union all
select 246 as Id, '11.20' as Code, 'Предоставление услуг по добыче нефти и газа' as Name, 236 ParentId, 0 as LastUpdateTick union all
select 247 as Id, '11.20.1' as Code, 'Предоставление услуг по бурению, связанному с добычей нефти, газа и  газового конденсата' as Name, 246 ParentId, 0 as LastUpdateTick union all
select 248 as Id, '11.20.2' as Code, 'Предоставление услуг по монтажу, ремонту и демонтажу буровых вышек' as Name, 246 ParentId, 0 as LastUpdateTick union all
select 249 as Id, '11.20.3' as Code, 'Предоставление услуг по доразведке месторождений нефти и газа на  особых экономических условиях (по соглашению о разделе продукции - СРП)' as Name, 246 ParentId, 0 as LastUpdateTick union all
select 250 as Id, '11.20.4' as Code, 'Предоставление прочих услуг, связанных с добычей нефти и газа' as Name, 246 ParentId, 0 as LastUpdateTick union all
select 251 as Id, '12' as Code, 'Добыча урановой и ториевой руд' as Name, 103 ParentId, 0 as LastUpdateTick union all
select 252 as Id, '12.0' as Code, 'Добыча урановой и ториевой руд' as Name, 251 ParentId, 0 as LastUpdateTick union all
select 253 as Id, '12.00' as Code, 'Добыча урановой и ториевой руд' as Name, 251 ParentId, 0 as LastUpdateTick union all
select 254 as Id, '12.00.1' as Code, 'Добыча и обогащение (сортировка) урановых руд' as Name, 253 ParentId, 0 as LastUpdateTick union all
select 255 as Id, '12.00.11' as Code, 'Добыча урановых руд подземным способом, включая способы подземного  и кучного выщелачивания' as Name, 253 ParentId, 0 as LastUpdateTick union all
select 256 as Id, '12.00.12' as Code, 'Добыча урановых руд открытым способом, включая способ кучного  выщелачивания' as Name, 253 ParentId, 0 as LastUpdateTick union all
select 257 as Id, '12.00.2' as Code, 'Добыча и обогащение ториевых руд' as Name, 253 ParentId, 0 as LastUpdateTick union all
select 258 as Id, '13' as Code, 'Добыча металлических руд' as Name, 104 ParentId, 0 as LastUpdateTick union all
select 259 as Id, '13.1' as Code, 'Добыча и обогащение железных руд' as Name, 258 ParentId, 0 as LastUpdateTick union all
select 260 as Id, '13.10' as Code, 'Добыча и обогащение железных руд' as Name, 258 ParentId, 0 as LastUpdateTick union all
select 261 as Id, '13.10.1' as Code, 'Добыча железных руд подземным способом' as Name, 260 ParentId, 0 as LastUpdateTick union all
select 262 as Id, '13.10.2' as Code, 'Добыча железных руд открытым способом' as Name, 260 ParentId, 0 as LastUpdateTick union all
select 263 as Id, '13.2' as Code, 'Добыча и обогащение руд цветных металлов, кроме урановой и ториевой руд' as Name, 258 ParentId, 0 as LastUpdateTick union all
select 264 as Id, '13.20' as Code, 'Добыча и обогащение руд цветных металлов, кроме урановой и ториевой руд' as Name, 258 ParentId, 0 as LastUpdateTick union all
select 265 as Id, '13.20.1' as Code, 'Добыча и обогащение медной руды' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 266 as Id, '13.20.2' as Code, 'Добыча и обогащение никелевой и кобальтовой руд' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 267 as Id, '13.20.3' as Code, 'Добыча и обогащение алюминийсодержащего сырья (бокситов и нефелин- апатитовых руд)' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 268 as Id, '13.20.31' as Code, 'Добыча алюминийсодержащего сырья подземным способом' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 269 as Id, '13.20.32' as Code, 'Добыча алюминийсодержащего сырья открытым способом' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 270 as Id, '13.20.33' as Code, 'Обогащение нефелин-апатитовых руд' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 271 as Id, '13.20.4' as Code, 'Добыча руд и песков драгоценных металлов и руд редких металлов' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 272 as Id, '13.20.41' as Code, 'Добыча руд и песков драгоценных металлов (золота, серебра и металлов  платиновой группы)' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 273 as Id, '13.20.42' as Code, 'Добыча и обогащение руд редких металлов (циркония, тантала, ниобия и  др.)' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 274 as Id, '13.20.5' as Code, 'Добыча и обогащение свинцово-цинковой руды' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 275 as Id, '13.20.6' as Code, 'Добыча и обогащение оловянной руды' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 276 as Id, '13.20.7' as Code, 'Добыча и обогащение титаномагниевого сырья' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 277 as Id, '13.20.8' as Code, 'Добыча и обогащение вольфраммолибденовой руды' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 278 as Id, '13.20.9' as Code, 'Добыча и обогащение руд прочих цветных металлов' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 279 as Id, '14' as Code, 'Добыча прочих полезных ископаемых' as Name, 104 ParentId, 0 as LastUpdateTick union all
select 280 as Id, '14.1' as Code, 'Разработка каменных карьеров' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 281 as Id, '14.11' as Code, 'Добыча камня для памятников и строительства' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 282 as Id, '14.12' as Code, 'Добыча известняка, гипсового камня и мела' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 283 as Id, '14.13' as Code, 'Добыча сланцев' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 284 as Id, '14.2' as Code, 'Добыча гравия, песка и глины' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 285 as Id, '14.21' as Code, 'Разработка гравийных и песчаных карьеров' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 286 as Id, '14.22' as Code, 'Добыча глины и каолина' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 287 as Id, '14.3' as Code, 'Добыча минерального сырья для химических производств и производства  удобрений' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 288 as Id, '14.30' as Code, 'Добыча минерального сырья для химических производств и производства удобрений' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 289 as Id, '14.4' as Code, 'Добыча и производство соли' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 290 as Id, '14.40' as Code, 'Добыча и производство соли' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 291 as Id, '14.5' as Code, 'Добыча прочих полезных ископаемых, не включенных в другие группировки' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 292 as Id, '14.50' as Code, 'Добыча прочих полезных ископаемых, не включенных в другие группировки' as Name, 279 ParentId, 0 as LastUpdateTick union all
select 293 as Id, '14.50.1' as Code, 'Добыча природного асфальтита и природного битума' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 294 as Id, '14.50.2' as Code, 'Добыча драгоценных и полудрагоценных камней; добыча природных  абразивов, пемзы, асбеста, слюды, кварца и прочих неметаллических минералов, не  включенных в другие группировки' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 295 as Id, '14.50.21' as Code, 'Добыча драгоценных и полудрагоценных камней, кроме алмазов,  самоцветов и янтаря' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 296 as Id, '14.50.22' as Code, 'Добыча алмазов' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 297 as Id, '14.50.23' as Code, 'Добыча природных абразивов, кроме алмазов, пемзы, наждака' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 298 as Id, '14.50.24' as Code, 'Добыча вермикулита' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 299 as Id, '14.50.25' as Code, 'Добыча мусковита' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 300 as Id, '14.50.26' as Code, 'Добыча асбеста' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 301 as Id, '14.50.27' as Code, 'Добыча пьезокварца' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 302 as Id, '14.50.28' as Code, 'Добыча гранулированного кварца' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 303 as Id, '14.50.29' as Code, 'Добыча и обогащение горных пород, содержащих графит и прочие  полезные ископаемые, не включенные в другие группировки' as Name, 292 ParentId, 0 as LastUpdateTick union all
select 304 as Id, '15' as Code, 'Производство пищевых продуктов, включая напитки' as Name, 106 ParentId, 0 as LastUpdateTick union all
select 305 as Id, '15.1' as Code, 'Производство мяса и мясопродуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 306 as Id, '15.11' as Code, 'Производство мяса' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 307 as Id, '15.11.1' as Code, 'Производство мяса и пищевых субпродуктов крупного рогатого скота,  свиней, овец, коз, животных семейства лошадиных' as Name, 306 ParentId, 0 as LastUpdateTick union all
select 308 as Id, '15.11.2' as Code, 'Производство щипаной шерсти, сырых шкур и кож крупного рогатого скота,  животных семейства лошадиных, овец, коз и свиней' as Name, 306 ParentId, 0 as LastUpdateTick union all
select 309 as Id, '15.11.3' as Code, 'Производство пищевых животных жиров' as Name, 306 ParentId, 0 as LastUpdateTick union all
select 310 as Id, '15.11.4' as Code, 'Производство непищевых субпродуктов' as Name, 306 ParentId, 0 as LastUpdateTick union all
select 311 as Id, '15.12' as Code, 'Производство мяса сельскохозяйственной птицы и кроликов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 312 as Id, '15.12.1' as Code, 'Производство мяса и пищевых субпродуктов сельскохозяйственной птицы и кроликов' as Name, 311 ParentId, 0 as LastUpdateTick union all
select 313 as Id, '15.12.2' as Code, 'Производство пера и пуха' as Name, 311 ParentId, 0 as LastUpdateTick union all
select 314 as Id, '15.13' as Code, 'Производство продуктов из мяса и мяса птицы' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 315 as Id, '15.13.1' as Code, 'Производство готовых и консервированных продуктов из мяса, мяса птицы,  мясных субпродуктов и крови животных' as Name, 314 ParentId, 0 as LastUpdateTick union all
select 316 as Id, '15.13.9' as Code, 'Предоставление услуг по тепловой обработке и прочим способам  переработки мясных продуктов' as Name, 314 ParentId, 0 as LastUpdateTick union all
select 317 as Id, '15.2' as Code, 'Переработка и консервирование рыбо- и морепродуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 318 as Id, '15.20' as Code, 'Переработка и консервирование рыбо- и морепродуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 319 as Id, '15.3' as Code, 'Переработка и консервирование картофеля, фруктов и овощей' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 320 as Id, '15.31' as Code, 'Переработка и консервирование картофеля' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 321 as Id, '15.32' as Code, 'Производство фруктовых и овощных соков' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 322 as Id, '15.33' as Code, 'Переработка и консервирование фруктов и овощей, не включенных в другие  группировки' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 323 as Id, '15.33.1' as Code, 'Переработка и консервирование овощей' as Name, 322 ParentId, 0 as LastUpdateTick union all
select 324 as Id, '15.33.2' as Code, 'Переработка и консервирование фруктов и орехов' as Name, 322 ParentId, 0 as LastUpdateTick union all
select 325 as Id, '15.33.9' as Code, 'Предоставление услуг по тепловой обработке и прочим способам  подготовки овощей и фруктов для консервирования' as Name, 322 ParentId, 0 as LastUpdateTick union all
select 326 as Id, '15.4' as Code, 'Производство растительных и животных масел и жиров' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 327 as Id, '15.41' as Code, 'Производство неочищенных масел и жиров' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 328 as Id, '15.41.1' as Code, 'Производство технических животных жиров, рыбьего жира и жиров морских  млекопитающих' as Name, 327 ParentId, 0 as LastUpdateTick union all
select 329 as Id, '15.41.2' as Code, 'Производство неочищенных растительных масел' as Name, 327 ParentId, 0 as LastUpdateTick union all
select 330 as Id, '15.42' as Code, 'Производство рафинированных масел и жиров' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 331 as Id, '15.42.1' as Code, 'Производство растительных рафинированных масел и жиров' as Name, 330 ParentId, 0 as LastUpdateTick union all
select 332 as Id, '15.42.2' as Code, 'Производство растительного воска, кроме триглицеридов' as Name, 330 ParentId, 0 as LastUpdateTick union all
select 333 as Id, '15.43' as Code, 'Производство маргариновой продукции' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 334 as Id, '15.43.1' as Code, 'Производство маргарина' as Name, 333 ParentId, 0 as LastUpdateTick union all
select 335 as Id, '15.43.2' as Code, 'Производство комбинированных жиров' as Name, 333 ParentId, 0 as LastUpdateTick union all
select 336 as Id, '15.5' as Code, 'Производство молочных продуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 337 as Id, '15.51' as Code, 'Переработка молока и производство сыра' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 338 as Id, '15.51.1' as Code, 'Производство цельномолочной продукции' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 339 as Id, '15.51.11' as Code, 'Производство обработанного жидкого молока' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 340 as Id, '15.51.12' as Code, 'Производство сметаны и жидких сливок' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 341 as Id, '15.51.13' as Code, 'Производство кисло-молочной продукции' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 342 as Id, '15.51.14' as Code, 'Производство творога и сырково-творожных изделий' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 343 as Id, '15.51.2' as Code, 'Производство молока, сливок и других молочных продуктов в твердых  формах' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 344 as Id, '15.51.3' as Code, 'Производство коровьего масла' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 345 as Id, '15.51.4' as Code, 'Производство сыра' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 346 as Id, '15.51.5' as Code, 'Производство сгущенных молочных продуктов и молочных продуктов, не включенных в другие группировки' as Name, 337 ParentId, 0 as LastUpdateTick union all
select 347 as Id, '15.52' as Code, 'Производство мороженого' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 348 as Id, '15.6' as Code, 'Производство продуктов мукомольно-крупяной промышленности, крахмалов и  крахмалопродуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 349 as Id, '15.61' as Code, 'Производство продуктов мукомольно-крупяной промышленности' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 350 as Id, '15.61.1' as Code, 'Производство обработанного риса' as Name, 349 ParentId, 0 as LastUpdateTick union all
select 351 as Id, '15.61.2' as Code, 'Производство муки из зерновых и растительных культур и готовых мучных  смесей и теста для выпечки' as Name, 349 ParentId, 0 as LastUpdateTick union all
select 352 as Id, '15.61.3' as Code, 'Производство крупы, муки грубого помола, гранул и прочих продуктов из  зерновых культур' as Name, 349 ParentId, 0 as LastUpdateTick union all
select 353 as Id, '15.62' as Code, 'Производство кукурузного масла, крахмала и крахмалопродуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 354 as Id, '15.62.1' as Code, 'Производство кукурузного масла' as Name, 353 ParentId, 0 as LastUpdateTick union all
select 355 as Id, '15.62.2' as Code, 'Производство крахмала и крахмалопродуктов; производство сахаров и  сахарных сиропов, не включенных в другие группировки' as Name, 353 ParentId, 0 as LastUpdateTick union all
select 356 as Id, '15.7' as Code, 'Производство готовых кормов для животных' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 357 as Id, '15.71' as Code, 'Производство готовых кормов и их составляющих для животных,  содержащихся на фермах' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 358 as Id, '15.71.1' as Code, 'Производство готовых кормов (смешанных и несмешанных) для животных,  содержащихся на фермах' as Name, 357 ParentId, 0 as LastUpdateTick union all
select 359 as Id, '15.71.2' as Code, 'Производство кормового микробиологического белка, премиксов, кормовых  витаминов, антибиотиков, аминокислот и ферментов' as Name, 357 ParentId, 0 as LastUpdateTick union all
select 360 as Id, '15.72' as Code, 'Производство готовых кормов для домашних животных' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 361 as Id, '15.8' as Code, 'Производство прочих пищевых продуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 362 as Id, '15.81' as Code, 'Производство хлеба и мучных кондитерских изделий недлительного хранения' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 363 as Id, '15.82' as Code, 'Производство сухих хлебобулочных изделий и мучных кондитерских изделий  длительного хранения' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 364 as Id, '15.83' as Code, 'Производство сахара' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 365 as Id, '15.84' as Code, 'Производство какао, шоколада и сахаристых кондитерских изделий' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 366 as Id, '15.84.1' as Code, 'Производство какао' as Name, 365 ParentId, 0 as LastUpdateTick union all
select 367 as Id, '15.84.2' as Code, 'Производство шоколада и сахаристых кондитерских изделий' as Name, 365 ParentId, 0 as LastUpdateTick union all
select 368 as Id, '15.85' as Code, 'Производство макаронных изделий' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 369 as Id, '15.86' as Code, 'Производство чая и кофе' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 370 as Id, '15.87' as Code, 'Производство пряностей и приправ' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 371 as Id, '15.88' as Code, 'Производство детского питания и диетических пищевых продуктов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 372 as Id, '15.89' as Code, 'Производство прочих пищевых продуктов, не включенных в другие  группировки' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 373 as Id, '15.89.1' as Code, 'Производство готовых к употреблению пищевых продуктов и заготовок для их приготовления, не включенных в другие группировки' as Name, 372 ParentId, 0 as LastUpdateTick union all
select 374 as Id, '15.89.2' as Code, 'Производство растительных соков и экстрактов, пептических веществ,  растительных клеев и загустителей' as Name, 372 ParentId, 0 as LastUpdateTick union all
select 375 as Id, '15.89.3' as Code, 'Производство пищевых ферментов' as Name, 372 ParentId, 0 as LastUpdateTick union all
select 376 as Id, '15.9' as Code, 'Производство напитков' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 377 as Id, '15.91' as Code, 'Производство дистиллированных алкогольных напитков' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 378 as Id, '15.92' as Code, 'Производство этилового спирта из сброженных материалов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 379 as Id, '15.93' as Code, 'Производство виноградного вина' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 380 as Id, '15.94' as Code, 'Производство сидра и прочих плодовых вин' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 381 as Id, '15.95' as Code, 'Производство прочих недистиллированных напитков из сброженных  материалов' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 382 as Id, '15.96' as Code, 'Производство пива' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 383 as Id, '15.97' as Code, 'Производство солода' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 384 as Id, '15.98' as Code, 'Производство минеральных вод и других безалкогольных напитков' as Name, 304 ParentId, 0 as LastUpdateTick union all
select 385 as Id, '15.98.1' as Code, 'Производство минеральных вод' as Name, 384 ParentId, 0 as LastUpdateTick union all
select 386 as Id, '15.98.2' as Code, 'Производство безалкогольных напитков, кроме минеральных вод' as Name, 384 ParentId, 0 as LastUpdateTick union all
select 387 as Id, '16' as Code, 'Производство табачных изделий' as Name, 106 ParentId, 0 as LastUpdateTick union all
select 388 as Id, '16.0' as Code, 'Производство табачных изделий' as Name, 387 ParentId, 0 as LastUpdateTick union all
select 389 as Id, '16.00' as Code, 'Производство табачных изделий' as Name, 387 ParentId, 0 as LastUpdateTick union all
select 390 as Id, '17' as Code, 'Текстильное производство' as Name, 107 ParentId, 0 as LastUpdateTick union all
select 391 as Id, '17.1' as Code, 'Прядение текстильных волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 392 as Id, '17.11' as Code, 'Прядение хлопчатобумажных волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 393 as Id, '17.12' as Code, 'Кардное прядение шерстяных волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 394 as Id, '17.13' as Code, 'Гребенное прядение шерстяных волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 395 as Id, '17.14' as Code, 'Прядение льняных волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 396 as Id, '17.15' as Code, 'Изготовление натуральных шелковых, искусственных и синтетических  волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 397 as Id, '17.16' as Code, 'Производство швейных ниток' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 398 as Id, '17.17' as Code, 'Подготовка и прядение прочих текстильных волокон' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 399 as Id, '17.2' as Code, 'Ткацкое производство' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 400 as Id, '17.21' as Code, 'Производство хлопчатобумажных тканей' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 401 as Id, '17.22' as Code, 'Производство шерстяных тканей из волокон кардного прядения' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 402 as Id, '17.23' as Code, 'Производство шерстяных тканей из волокон гребенного прядения' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 403 as Id, '17.24' as Code, 'Производство шелковых тканей' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 404 as Id, '17.25' as Code, 'Производство прочих текстильных тканей' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 405 as Id, '17.3' as Code, 'Отделка тканей и текстильных изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 406 as Id, '17.30' as Code, 'Отделка тканей и текстильных изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 407 as Id, '17.4' as Code, 'Производство готовых текстильных изделий, кроме одежды' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 408 as Id, '17.40' as Code, 'Производство готовых текстильных изделий, кроме одежды' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 409 as Id, '17.5' as Code, 'Производство прочих текстильных изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 410 as Id, '17.51' as Code, 'Производство ковров и ковровых изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 411 as Id, '17.52' as Code, 'Производство канатов, веревок, шпагата и сетей' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 412 as Id, '17.53' as Code, 'Производство нетканых текстильных материалов и изделий из них' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 413 as Id, '17.54' as Code, 'Производство прочих текстильных изделий, не включенных в другие  группировки' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 414 as Id, '17.54.1' as Code, 'Производство тюля, кружев, узких тканей, вышивок' as Name, 413 ParentId, 0 as LastUpdateTick union all
select 415 as Id, '17.54.2' as Code, 'Производство фетра и войлока' as Name, 413 ParentId, 0 as LastUpdateTick union all
select 416 as Id, '17.54.3' as Code, 'Производство текстильных изделий различного назначения, не включенных  в другие группировки' as Name, 413 ParentId, 0 as LastUpdateTick union all
select 417 as Id, '17.6' as Code, 'Производство трикотажного полотна' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 418 as Id, '17.60' as Code, 'Производство трикотажного полотна' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 419 as Id, '17.7' as Code, 'Производство трикотажных изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 420 as Id, '17.71' as Code, 'Производство чулочно-носочных изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 421 as Id, '17.72' as Code, 'Производство трикотажных джемперов, жакетов, жилетов, кардиганов и  аналогичных изделий' as Name, 390 ParentId, 0 as LastUpdateTick union all
select 422 as Id, '18' as Code, 'Производство одежды; выделка и крашение меха' as Name, 107 ParentId, 0 as LastUpdateTick union all
select 423 as Id, '18.1' as Code, 'Производство одежды из кожи' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 424 as Id, '18.10' as Code, 'Производство одежды из кожи' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 425 as Id, '18.2' as Code, 'Производство одежды из текстильных материалов и аксессуаров одежды' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 426 as Id, '18.21' as Code, 'Производство спецодежды' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 427 as Id, '18.22' as Code, 'Производство верхней одежды' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 428 as Id, '18.22.1' as Code, 'Производство верхней трикотажной одежды' as Name, 427 ParentId, 0 as LastUpdateTick union all
select 429 as Id, '18.22.2' as Code, 'Производство верхней одежды из тканей для мужчин и мальчиков' as Name, 427 ParentId, 0 as LastUpdateTick union all
select 430 as Id, '18.22.3' as Code, 'Производство верхней одежды из тканей для женщин и девочек' as Name, 427 ParentId, 0 as LastUpdateTick union all
select 431 as Id, '18.23' as Code, 'Производство нательного белья' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 432 as Id, '18.23.1' as Code, 'Производство трикотажного нательного белья' as Name, 431 ParentId, 0 as LastUpdateTick union all
select 433 as Id, '18.23.2' as Code, 'Производство нательного белья из тканей' as Name, 431 ParentId, 0 as LastUpdateTick union all
select 434 as Id, '18.24' as Code, 'Производство прочей одежды и аксессуаров' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 435 as Id, '18.24.1' as Code, 'Производство трикотажной одежды для новорожденных детей, спортивной  одежды и аксессуаров одежды' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 436 as Id, '18.24.11' as Code, 'Производство трикотажной одежды и аксессуаров одежды для  новорожденных детей' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 437 as Id, '18.24.12' as Code, 'Производство трикотажных спортивных костюмов, лыжных костюмов, купальников и прочей трикотажной одежды' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 438 as Id, '18.24.13' as Code, 'Производство трикотажных перчаток, варежек и рукавиц' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 439 as Id, '18.24.14' as Code, 'Производство прочих трикотажных аксессуаров одежды, в том числе  платков, шарфов, галстуков и прочих аналогичных изделий' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 440 as Id, '18.24.2' as Code, 'Производство одежды для новорожденных детей, спортивной одежды и  аксессуаров одежды из тканей' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 441 as Id, '18.24.21' as Code, 'Производство одежды для новорожденных детей из тканей' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 442 as Id, '18.24.22' as Code, 'Производство спортивной одежды из тканей' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 443 as Id, '18.24.23' as Code, 'Производство аксессуаров одежды, в том числе платков, шарфов,  галстуков, перчаток и прочих аналогичных изделий из тканей' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 444 as Id, '18.24.3' as Code, 'Производство аксессуаров одежды из кожи; производство одежды из фетра  или нетканых материалов; производство одежды из текстильных материалов с  покрытием' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 445 as Id, '18.24.31' as Code, 'Производство аксессуаров одежды из натуральной или композиционной  кожи' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 446 as Id, '18.24.32' as Code, 'Производство одежды из фетра, нетканых материалов, из текстильных  материалов с пропиткой или покрытием' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 447 as Id, '18.24.4' as Code, 'Производство головных уборов' as Name, 434 ParentId, 0 as LastUpdateTick union all
select 448 as Id, '18.3' as Code, 'Выделка и крашение меха; производство меховых изделий' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 449 as Id, '18.30' as Code, 'Выделка и крашение меха; производство меховых изделий' as Name, 422 ParentId, 0 as LastUpdateTick union all
select 450 as Id, '18.30.1' as Code, 'Выделка и крашение меха' as Name, 449 ParentId, 0 as LastUpdateTick union all
select 451 as Id, '18.30.2' as Code, 'Производство одежды, аксессуаров и прочих изделий из меха, кроме головных уборов' as Name, 449 ParentId, 0 as LastUpdateTick union all
select 452 as Id, '18.30.3' as Code, 'Производство искусственного меха и изделий из него' as Name, 449 ParentId, 0 as LastUpdateTick union all
select 453 as Id, '18.30.31' as Code, 'Производство искусственного меха' as Name, 449 ParentId, 0 as LastUpdateTick union all
select 454 as Id, '18.30.32' as Code, 'Производство изделий из искусственного меха' as Name, 449 ParentId, 0 as LastUpdateTick union all
select 455 as Id, '19' as Code, 'Производство кожи, изделий из кожи и производство обуви' as Name, 108 ParentId, 0 as LastUpdateTick union all
select 456 as Id, '19.1' as Code, 'Дубление и отделка кожи' as Name, 455 ParentId, 0 as LastUpdateTick union all
select 457 as Id, '19.10' as Code, 'Дубление и отделка кожи' as Name, 455 ParentId, 0 as LastUpdateTick union all
select 458 as Id, '19.2' as Code, 'Производство чемоданов, сумок и аналогичных изделий из кожи и других  материалов; производство шорно-седельных и других изделий из кожи' as Name, 455 ParentId, 0 as LastUpdateTick union all
select 459 as Id, '19.20' as Code, 'Производство чемоданов, сумок и аналогичных изделий из кожи и других  материалов; производство шорно-седельных и других изделий из кожи' as Name, 455 ParentId, 0 as LastUpdateTick union all
select 460 as Id, '19.3' as Code, 'Производство обуви' as Name, 455 ParentId, 0 as LastUpdateTick union all
select 461 as Id, '19.30' as Code, 'Производство обуви' as Name, 455 ParentId, 0 as LastUpdateTick union all
select 462 as Id, '20' as Code, 'Обработка древесины и производство изделий из дерева и пробки, кроме мебели' as Name, 109 ParentId, 0 as LastUpdateTick union all
select 463 as Id, '20.1' as Code, 'Распиловка и строгание древесины; пропитка древесины' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 464 as Id, '20.10' as Code, 'Распиловка и строгание древесины; пропитка древесины' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 465 as Id, '20.10.1' as Code, 'Производство пиломатериалов, кроме профилированных, толщиной более 6 мм; производство непропитанных железнодорожных и трамвайных шпал из древесины' as Name, 464 ParentId, 0 as LastUpdateTick union all
select 466 as Id, '20.10.2' as Code, 'Производство пиломатериалов, профилированных по кромке или по пласти;  производство древесной шерсти, древесной муки; производство технологической  щепы или стружки' as Name, 464 ParentId, 0 as LastUpdateTick union all
select 467 as Id, '20.10.3' as Code, 'Производство древесины, пропитанной или обработанной консервантами  или другими веществами' as Name, 464 ParentId, 0 as LastUpdateTick union all
select 468 as Id, '20.10.9' as Code, 'Предоставление услуг по пропитке древесины' as Name, 464 ParentId, 0 as LastUpdateTick union all
select 469 as Id, '20.2' as Code, 'Производство шпона, фанеры, плит, панелей' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 470 as Id, '20.20' as Code, 'Производство шпона, фанеры, плит, панелей' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 471 as Id, '20.20.1' as Code, 'Производство клееной фанеры, щитов, древесных плит и панелей' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 472 as Id, '20.20.2' as Code, 'Производство шпона, листов для клееной фанеры и модифицированной  древесины' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 473 as Id, '20.20.21' as Code, 'Производство шпона и листов для клееной фанеры' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 474 as Id, '20.20.22' as Code, 'Производство модифицированной древесины' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 475 as Id, '20.3' as Code, 'Производство деревянных строительных конструкций, включая сборные  деревянные строения, и столярных изделий' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 476 as Id, '20.30' as Code, 'Производство деревянных строительных конструкций, включая сборные  деревянные строения, и столярных изделий' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 477 as Id, '20.30.1' as Code, 'Производство деревянных строительных конструкций и столярных изделий' as Name, 476 ParentId, 0 as LastUpdateTick union all
select 478 as Id, '20.30.2' as Code, 'Производство сборных деревянных строений' as Name, 476 ParentId, 0 as LastUpdateTick union all
select 479 as Id, '20.4' as Code, 'Производство деревянной тары' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 480 as Id, '20.40' as Code, 'Производство деревянной тары' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 481 as Id, '20.5' as Code, 'Производство прочих изделий из дерева и пробки, соломки и материалов для  плетения' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 482 as Id, '20.51' as Code, 'Производство прочих изделий из дерева' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 483 as Id, '20.51.1' as Code, 'Производство деревянных инструментов, корпусов и рукояток  инструментов, рукояток щеток и метелок, обувных колодок и растяжек для обуви' as Name, 482 ParentId, 0 as LastUpdateTick union all
select 484 as Id, '20.51.2' as Code, 'Производство деревянных столовых и кухонных принадлежностей' as Name, 482 ParentId, 0 as LastUpdateTick union all
select 485 as Id, '20.51.3' as Code, 'Производство деревянных статуэток и украшений из дерева, мозаики и  инкрустированного дерева, шкатулок, футляров для ювелирных изделий или  ножей' as Name, 482 ParentId, 0 as LastUpdateTick union all
select 486 as Id, '20.51.4' as Code, 'Производство деревянных рам для картин, фотографий, зеркал или аналогичных предметов и прочих изделий из дерева' as Name, 482 ParentId, 0 as LastUpdateTick union all
select 487 as Id, '20.52' as Code, 'Производство изделий из пробки, соломки и материалов для плетения' as Name, 462 ParentId, 0 as LastUpdateTick union all
select 488 as Id, '21' as Code, 'Производство целлюлозы, древесной массы, бумаги, картона и изделий из них' as Name, 110 ParentId, 0 as LastUpdateTick union all
select 489 as Id, '21.1' as Code, 'Производство целлюлозы, древесной массы, бумаги и картона' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 490 as Id, '21.11' as Code, 'Производство целлюлозы и древесной массы' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 491 as Id, '21.12' as Code, 'Производство бумаги и картона' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 492 as Id, '21.2' as Code, 'Производство изделий из бумаги и картона' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 493 as Id, '21.21' as Code, 'Производство гофрированного картона, бумажной и картонной тары' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 494 as Id, '21.22' as Code, 'Производство бумажных изделий хозяйственно-бытового и санитарно- гигиенического назначения' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 495 as Id, '21.23' as Code, 'Производство писчебумажных изделий' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 496 as Id, '21.24' as Code, 'Производство обоев' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 497 as Id, '21.25' as Code, 'Производство прочих изделий из бумаги и картона' as Name, 488 ParentId, 0 as LastUpdateTick union all
select 498 as Id, '22' as Code, 'Издательская и полиграфическая деятельность, тиражирование записанных носителей информации' as Name, 110 ParentId, 0 as LastUpdateTick union all
select 499 as Id, '22.1' as Code, 'Издательская деятельность' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 500 as Id, '22.11' as Code, 'Издание книг' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 501 as Id, '22.11.1' as Code, 'Издание книг, брошюр, буклетов и аналогичных публикаций, в том числе  для слепых' as Name, 500 ParentId, 0 as LastUpdateTick union all
select 502 as Id, '22.11.2' as Code, 'Издание карт и атласов, в том числе для слепых' as Name, 500 ParentId, 0 as LastUpdateTick union all
select 503 as Id, '22.11.3' as Code, 'Издание нот, в том числе для слепых' as Name, 500 ParentId, 0 as LastUpdateTick union all
select 504 as Id, '22.12' as Code, 'Издание газет' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 505 as Id, '22.13' as Code, 'Издание журналов и периодических публикаций' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 506 as Id, '22.14' as Code, 'Издание звукозаписей' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 507 as Id, '22.15' as Code, 'Прочие виды издательской деятельности' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 508 as Id, '22.2' as Code, 'Полиграфическая деятельность и предоставление услуг в этой области' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 509 as Id, '22.21' as Code, 'Печатание газет' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 510 as Id, '22.22' as Code, 'Полиграфическая деятельность, не включенная в другие группировки' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 511 as Id, '22.23' as Code, 'Брошюровочно-переплетная деятельность' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 512 as Id, '22.24' as Code, 'Подготовка к печати' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 513 as Id, '22.25' as Code, 'Дополнительная деятельность, связанная с печатанием' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 514 as Id, '22.3' as Code, 'Копирование записанных носителей информации' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 515 as Id, '22.31' as Code, 'Копирование звукозаписей' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 516 as Id, '22.32' as Code, 'Копирование видеозаписей' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 517 as Id, '22.33' as Code, 'Копирование машинных носителей информации' as Name, 498 ParentId, 0 as LastUpdateTick union all
select 518 as Id, '23' as Code, 'Производство кокса, нефтепродуктов и ядерных материалов' as Name, 111 ParentId, 0 as LastUpdateTick union all
select 519 as Id, '23.1' as Code, 'Производство кокса' as Name, 518 ParentId, 0 as LastUpdateTick union all
select 520 as Id, '23.10' as Code, 'Производство кокса' as Name, 518 ParentId, 0 as LastUpdateTick union all
select 521 as Id, '23.2' as Code, 'Производство нефтепродуктов' as Name, 518 ParentId, 0 as LastUpdateTick union all
select 522 as Id, '23.20' as Code, 'Производство нефтепродуктов' as Name, 518 ParentId, 0 as LastUpdateTick union all
select 523 as Id, '23.3' as Code, 'Производство ядерных материалов' as Name, 518 ParentId, 0 as LastUpdateTick union all
select 524 as Id, '23.30' as Code, 'Производство ядерных материалов' as Name, 518 ParentId, 0 as LastUpdateTick union all
select 525 as Id, '24' as Code, 'Химическое производство' as Name, 112 ParentId, 0 as LastUpdateTick union all
select 526 as Id, '24.1' as Code, 'Производство основных химических веществ' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 527 as Id, '24.11' as Code, 'Производство промышленных газов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 528 as Id, '24.12' as Code, 'Производство красителей и пигментов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 529 as Id, '24.13' as Code, 'Производство прочих основных неорганических химических веществ' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 530 as Id, '24.14' as Code, 'Производство прочих основных органических химических веществ' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 531 as Id, '24.14.1' as Code, 'Производство синтетического и гидролизного этилового спирта' as Name, 530 ParentId, 0 as LastUpdateTick union all
select 532 as Id, '24.14.2' as Code, 'Производство прочих основных органических химических веществ, не включенных в другие группировки' as Name, 530 ParentId, 0 as LastUpdateTick union all
select 533 as Id, '24.15' as Code, 'Производство удобрений и азотных соединений' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 534 as Id, '24.16' as Code, 'Производство пластмасс и синтетических смол в первичных формах' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 535 as Id, '24.17' as Code, 'Производство синтетического каучука' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 536 as Id, '24.2' as Code, 'Производство химических средств защиты растений (пестицидов) и прочих  агрохимических продуктов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 537 as Id, '24.20' as Code, 'Производство химических средств защиты растений (пестицидов) и прочих агрохимических продуктов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 538 as Id, '24.3' as Code, 'Производство красок и лаков' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 539 as Id, '24.30' as Code, 'Производство красок и лаков' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 540 as Id, '24.30.1' as Code, 'Производство красок и лаков на основе полимеров' as Name, 539 ParentId, 0 as LastUpdateTick union all
select 541 as Id, '24.30.2' as Code, 'Производство прочих красок, лаков, эмалей и связанных с ними продуктов' as Name, 539 ParentId, 0 as LastUpdateTick union all
select 542 as Id, '24.4' as Code, 'Производство фармацевтической продукции' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 543 as Id, '24.41' as Code, 'Производство основной фармацевтической продукции' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 544 as Id, '24.42' as Code, 'Производство фармацевтических препаратов и материалов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 545 as Id, '24.42.1' as Code, 'Производство медикаментов' as Name, 544 ParentId, 0 as LastUpdateTick union all
select 546 as Id, '24.42.2' as Code, 'Производство прочих фармацевтических продуктов и изделий медицинского назначения' as Name, 544 ParentId, 0 as LastUpdateTick union all
select 547 as Id, '24.5' as Code, 'Производство мыла; моющих, чистящих и полирующих средств; парфюмерных  и косметических средств' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 548 as Id, '24.51' as Code, 'Производство глицерина; мыла; моющих, чистящих, полирующих средств' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 549 as Id, '24.51.1' as Code, 'Производство глицерина' as Name, 548 ParentId, 0 as LastUpdateTick union all
select 550 as Id, '24.51.2' as Code, 'Производство органических поверхностно-активных веществ, кроме мыла' as Name, 548 ParentId, 0 as LastUpdateTick union all
select 551 as Id, '24.51.3' as Code, 'Производство мыла и моющих средств' as Name, 548 ParentId, 0 as LastUpdateTick union all
select 552 as Id, '24.51.4' as Code, 'Производство средств для ароматизации и дезодорирования воздуха;  производство полирующих и чистящих средств, восков' as Name, 548 ParentId, 0 as LastUpdateTick union all
select 553 as Id, '24.52' as Code, 'Производство парфюмерных и косметических средств' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 554 as Id, '24.6' as Code, 'Производство прочих химических продуктов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 555 as Id, '24.61' as Code, 'Производство взрывчатых веществ' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 556 as Id, '24.62' as Code, 'Производство клеев и желатина' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 557 as Id, '24.63' as Code, 'Производство эфирных масел' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 558 as Id, '24.64' as Code, 'Производство фотоматериалов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 559 as Id, '24.65' as Code, 'Производство готовых незаписанных носителей информации' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 560 as Id, '24.66' as Code, 'Производство прочих химических продуктов' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 561 as Id, '24.66.1' as Code, 'Производство химически модифицированных животных или растительных  жиров и масел (включая олифу), непищевых смесей животных или растительных  жиров и масел' as Name, 560 ParentId, 0 as LastUpdateTick union all
select 562 as Id, '24.66.2' as Code, 'Производство чернил для письма и рисования' as Name, 560 ParentId, 0 as LastUpdateTick union all
select 563 as Id, '24.66.3' as Code, 'Производство смазочных материалов, присадок к смазочным материалам и  антифризов' as Name, 560 ParentId, 0 as LastUpdateTick union all
select 564 as Id, '24.66.4' as Code, 'Производство прочих химических продуктов, не включенных в другие группировки' as Name, 560 ParentId, 0 as LastUpdateTick union all
select 565 as Id, '24.7' as Code, 'Производство искусственных и синтетических волокон' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 566 as Id, '24.70' as Code, 'Производство искусственных и синтетических волокон' as Name, 525 ParentId, 0 as LastUpdateTick union all
select 567 as Id, '25' as Code, 'Производство резиновых и пластмассовых изделий' as Name, 113 ParentId, 0 as LastUpdateTick union all
select 568 as Id, '25.1' as Code, 'Производство резиновых изделий' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 569 as Id, '25.11' as Code, 'Производство резиновых шин, покрышек и камер' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 570 as Id, '25.12' as Code, 'Восстановление резиновых шин и покрышек' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 571 as Id, '25.13' as Code, 'Производство прочих резиновых изделий' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 572 as Id, '25.13.1' as Code, 'Производство регенерированной резины в первичной форме или в виде  пластин, листов или полос (лент)' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 573 as Id, '25.13.2' as Code, 'Производство невулканизированного каучука и изделий из него;  производство резины в виде нити, корда, пластин, листов, полос, стержней и  профилей' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 574 as Id, '25.13.3' as Code, 'Производство труб, трубок, рукавов и шлангов из резины' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 575 as Id, '25.13.4' as Code, 'Производство конвейерных лент и приводных ремней из резины' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 576 as Id, '25.13.5' as Code, 'Производство прорезиненных текстильных материалов, кроме кордной ткани' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 577 as Id, '25.13.6' as Code, 'Производство предметов одежды и ее аксессуаров из резин' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 578 as Id, '25.13.7' as Code, 'Производство изделий из резины, не включенных в другие группировки;  производство эбонита и изделий из него' as Name, 571 ParentId, 0 as LastUpdateTick union all
select 579 as Id, '25.2' as Code, 'Производство пластмассовых изделий' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 580 as Id, '25.21' as Code, 'Производство пластмассовых плит, полос, труб и профилей' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 581 as Id, '25.22' as Code, 'Производство пластмассовых изделий для упаковывания товаров' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 582 as Id, '25.23' as Code, 'Производство пластмассовых изделий, используемых в строительстве' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 583 as Id, '25.24' as Code, 'Производство прочих пластмассовых изделий' as Name, 567 ParentId, 0 as LastUpdateTick union all
select 584 as Id, '25.24.1' as Code, 'Производство предметов одежды и ее аксессуаров, включая перчатки, из  пластмасс' as Name, 583 ParentId, 0 as LastUpdateTick union all
select 585 as Id, '25.24.2' as Code, 'Производство прочих изделий из пластмасс, не включенных в другие  группировки' as Name, 583 ParentId, 0 as LastUpdateTick union all
select 586 as Id, '25.24.9' as Code, 'Предоставление услуг в области производства пластмассовых деталей' as Name, 583 ParentId, 0 as LastUpdateTick union all
select 587 as Id, '26' as Code, 'Производство прочих неметаллических минеральных продуктов' as Name, 114 ParentId, 0 as LastUpdateTick union all
select 588 as Id, '26.1' as Code, 'Производство стекла и изделий из стекла' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 589 as Id, '26.11' as Code, 'Производство листового стекла' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 590 as Id, '26.12' as Code, 'Формование и обработка листового стекла' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 591 as Id, '26.13' as Code, 'Производство полых стеклянных изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 592 as Id, '26.14' as Code, 'Производство стекловолокна' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 593 as Id, '26.15' as Code, 'Производство и обработка прочих стеклянных изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 594 as Id, '26.15.1' as Code, 'Производство необработанного стекла в блоках, в виде шаров, стержней,  труб или трубок' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 595 as Id, '26.15.2' as Code, 'Производство блоков для мощения, стеклоблоков, плит и прочих изделий из прессованного или отформованного стекла, используемых в строительстве; производство стекла для витражей; производство многоячеистого стекла или пеностекла в блоках, плитах и аналогичных формах' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 596 as Id, '26.15.3' as Code, 'Производство открытых стеклянных колб: колб для электрических ламп,  электронно-лучевых приборов или аналогичных изделий' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 597 as Id, '26.15.4' as Code, 'Производство стекол для часов или очков, не подвергнутых оптической  обработке' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 598 as Id, '26.15.5' as Code, 'Производство лабораторных, фармацевтических и гигиенических изделий  из стекла; производство ампул и прочих изделий из стекла медицинского  назначения' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 599 as Id, '26.15.6' as Code, 'Производство стеклянных деталей электрических ламп и осветительной  арматуры, световых указателей, световых табло и др.' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 600 as Id, '26.15.7' as Code, 'Производство электрических изоляторов из стекла' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 601 as Id, '26.15.8' as Code, 'Производство прочих изделий из стекла, не включенных в другие  группировки' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 602 as Id, '26.15.81' as Code, 'Производство оптических элементов из стекла без оптической обработки' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 603 as Id, '26.15.82' as Code, 'Производство кубиков для мозаичных или иных декоративных работ' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 604 as Id, '26.15.83' as Code, 'Производство стеклянных деталей для изготовления бижутерии;  производство стеклянного бисера и бусин; производство изделий, имитирующих  жемчуг, драгоценные и полудрагоценные камни; производство стеклянных  микросфер диаметром не более 1 мм' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 605 as Id, '26.15.84' as Code, 'Производство статуэток и прочих украшений из стекла, полученных методом выдувания из расплавленной стеклянной массы' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 606 as Id, '26.15.85' as Code, 'Производство изделий из стекла для промышленности и сельского  хозяйства, не включенных в другие группировки: баков, чанов, резервуаров,  цилиндров, змеевиков, желобов и т.п.' as Name, 593 ParentId, 0 as LastUpdateTick union all
select 607 as Id, '26.2' as Code, 'Производство керамических изделий, кроме используемых в строительстве' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 608 as Id, '26.21' as Code, 'Производство хозяйственных и декоративных керамических изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 609 as Id, '26.22' as Code, 'Производство керамических санитарно-технических изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 610 as Id, '26.23' as Code, 'Производство керамических электроизоляторов и изолирующей арматуры' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 611 as Id, '26.24' as Code, 'Производство прочих технических керамических изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 612 as Id, '26.25' as Code, 'Производство прочих керамических изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 613 as Id, '26.26' as Code, 'Производство огнеупоров' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 614 as Id, '26.3' as Code, 'Производство керамических плиток и плит' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 615 as Id, '26.30' as Code, 'Производство керамических плиток и плит' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 616 as Id, '26.4' as Code, 'Производство кирпича, черепицы и прочих строительных изделий из  обожженной глины' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 617 as Id, '26.40' as Code, 'Производство кирпича, черепицы и прочих строительных изделий из  обожженной глины' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 618 as Id, '26.5' as Code, 'Производство цемента, извести и гипса' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 619 as Id, '26.51' as Code, 'Производство цемента' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 620 as Id, '26.52' as Code, 'Производство извести' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 621 as Id, '26.53' as Code, 'Производство гипса' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 622 as Id, '26.6' as Code, 'Производство изделий из бетона, гипса и цемента' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 623 as Id, '26.61' as Code, 'Производство изделий из бетона для использования в строительстве' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 624 as Id, '26.62' as Code, 'Производство гипсовых изделий для использования в строительстве' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 625 as Id, '26.63' as Code, 'Производство товарного бетона' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 626 as Id, '26.64' as Code, 'Производство сухих бетонных смесей' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 627 as Id, '26.65' as Code, 'Производство изделий из асбестоцемента и волокнистого цемента' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 628 as Id, '26.66' as Code, 'Производство прочих изделий из бетона, гипса и цемента' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 629 as Id, '26.7' as Code, 'Резка, обработка и отделка декоративного и строительного камня' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 630 as Id, '26.70' as Code, 'Резка, обработка и отделка декоративного и строительного камня' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 631 as Id, '26.70.1' as Code, 'Резка, обработка и отделка камня для использования в строительстве, в  качестве дорожного покрытия' as Name, 630 ParentId, 0 as LastUpdateTick union all
select 632 as Id, '26.70.2' as Code, 'Резка, обработка и отделка камня для памятников' as Name, 630 ParentId, 0 as LastUpdateTick union all
select 633 as Id, '26.70.3' as Code, 'Производство гранул и порошков из природного камня' as Name, 630 ParentId, 0 as LastUpdateTick union all
select 634 as Id, '26.8' as Code, 'Производство прочей неметаллической минеральной продукции' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 635 as Id, '26.81' as Code, 'Производство абразивных изделий' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 636 as Id, '26.82' as Code, 'Производство прочей неметаллической минеральной продукции, не  включенной в другие группировки' as Name, 587 ParentId, 0 as LastUpdateTick union all
select 637 as Id, '26.82.1' as Code, 'Производство обработанных асбестовых волокон, смесей на основе  асбеста и изделий из них' as Name, 636 ParentId, 0 as LastUpdateTick union all
select 638 as Id, '26.82.2' as Code, 'Производство изделий из асфальта или аналогичных материалов' as Name, 636 ParentId, 0 as LastUpdateTick union all
select 639 as Id, '26.82.3' as Code, 'Производство битуминозных смесей на основе природного асфальта или  битума, нефтяного битума, минеральных смол или их пеков' as Name, 636 ParentId, 0 as LastUpdateTick union all
select 640 as Id, '26.82.4' as Code, 'Производство искусственного графита, коллоидного или полуколлоидного  графита, продуктов на основе графита или прочих форм углерода в виде  полуфабрикатов' as Name, 636 ParentId, 0 as LastUpdateTick union all
select 641 as Id, '26.82.5' as Code, 'Производство искусственного корунда' as Name, 636 ParentId, 0 as LastUpdateTick union all
select 642 as Id, '26.82.6' as Code, 'Производство минеральных тепло- и звукоизоляционных материалов и  изделий' as Name, 636 ParentId, 0 as LastUpdateTick union all
select 643 as Id, '27' as Code, 'Металлургическое производство' as Name, 115 ParentId, 0 as LastUpdateTick union all
select 644 as Id, '27.1' as Code, 'Производство чугуна, стали и ферросплавов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 645 as Id, '27.11' as Code, 'Производство чугуна и доменных ферросплавов' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 646 as Id, '27.12' as Code, 'Производство продуктов прямого восстановления железной руды' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 647 as Id, '27.13' as Code, 'Производство ферросплавов, кроме доменных' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 648 as Id, '27.14' as Code, 'Производство стали' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 649 as Id, '27.15' as Code, 'Производство полуфабрикатов (заготовок) для переката' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 650 as Id, '27.16' as Code, 'Производство стального проката горячекатаного и кованого' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 651 as Id, '27.16.1' as Code, 'Производство стального сортового проката горячекатаного и кованого' as Name, 650 ParentId, 0 as LastUpdateTick union all
select 652 as Id, '27.16.2' as Code, 'Производство стального горячекатаного листового (плоского) проката' as Name, 650 ParentId, 0 as LastUpdateTick union all
select 653 as Id, '27.17' as Code, 'Производство холоднокатаного плоского проката без защитных покрытий и с защитными покрытиями' as Name, 644 ParentId, 0 as LastUpdateTick union all
select 654 as Id, '27.2' as Code, 'Производство чугунных и стальных труб' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 655 as Id, '27.21' as Code, 'Производство чугунных труб и литых фитингов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 656 as Id, '27.22' as Code, 'Производство стальных труб и фитингов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 657 as Id, '27.3' as Code, 'Прочая первичная обработка чугуна и стали' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 658 as Id, '27.31' as Code, 'Производство холоднотянутых прутков и профилей' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 659 as Id, '27.32' as Code, 'Производство холоднокатаных узких полос и лент' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 660 as Id, '27.33' as Code, 'Производство гнутых стальных профилей' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 661 as Id, '27.34' as Code, 'Производство стальной проволоки' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 662 as Id, '27.35' as Code, 'Производство железных порошков, прочей металлопродукции из стального проката, не включенной в другие группировки' as Name, 657 ParentId, 0 as LastUpdateTick union all
select 663 as Id, '27.35.1' as Code, 'Производство железных порошков' as Name, 662 ParentId, 0 as LastUpdateTick union all
select 664 as Id, '27.35.2' as Code, 'Производство изделий из стального проката для верхнего строения железнодорожного пути' as Name, 662 ParentId, 0 as LastUpdateTick union all
select 665 as Id, '27.35.3' as Code, 'Производство профилей и конструкций шпунтового типа из стального проката' as Name, 662 ParentId, 0 as LastUpdateTick union all
select 666 as Id, '27.4' as Code, 'Производство цветных металлов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 667 as Id, '27.41' as Code, 'Производство драгоценных металлов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 668 as Id, '27.42' as Code, 'Производство алюминия' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 669 as Id, '27.42.1' as Code, 'Производство сырья для получения алюминия' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 670 as Id, '27.42.11' as Code, 'Производство оксида алюминия (глинозема)' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 671 as Id, '27.42.12' as Code, 'Производство криолита и фтористого алюминия' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 672 as Id, '27.42.2' as Code, 'Производство первичного алюминия' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 673 as Id, '27.42.3' as Code, 'Производство алюминиевых порошков' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 674 as Id, '27.42.4' as Code, 'Производство алюминиевых сплавов' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 675 as Id, '27.42.5' as Code, 'Производство полуфабрикатов из алюминия или алюминиевых сплавов' as Name, 668 ParentId, 0 as LastUpdateTick union all
select 676 as Id, '27.43' as Code, 'Производство свинца, цинка и олова' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 677 as Id, '27.44' as Code, 'Производство меди' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 678 as Id, '27.45' as Code, 'Производство прочих цветных металлов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 679 as Id, '27.5' as Code, 'Производство отливок' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 680 as Id, '27.51' as Code, 'Производство чугунных отливок' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 681 as Id, '27.52' as Code, 'Производство стальных отливок' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 682 as Id, '27.53' as Code, 'Производство отливок из легких металлов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 683 as Id, '27.54' as Code, 'Производство отливок из прочих цветных металлов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 684 as Id, '28' as Code, 'Производство готовых металлических изделий' as Name, 115 ParentId, 0 as LastUpdateTick union all
select 685 as Id, '28.1' as Code, 'Производство строительных металлических конструкций и изделий' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 686 as Id, '28.11' as Code, 'Производство строительных металлических конструкций' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 687 as Id, '28.12' as Code, 'Производство строительных металлических изделий' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 688 as Id, '28.2' as Code, 'Производство металлических резервуаров, радиаторов и котлов центрального  отопления' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 689 as Id, '28.21' as Code, 'Производство металлических цистерн, резервуаров и прочих емкостей' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 690 as Id, '28.22' as Code, 'Производство радиаторов и котлов центрального отопления' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 691 as Id, '28.22.1' as Code, 'Производство радиаторов' as Name, 690 ParentId, 0 as LastUpdateTick union all
select 692 as Id, '28.22.2' as Code, 'Производство котлов центрального отопления' as Name, 690 ParentId, 0 as LastUpdateTick union all
select 693 as Id, '28.22.9' as Code, 'Предоставление услуг по ремонту и техническому обслуживанию котлов  центрального отопления' as Name, 690 ParentId, 0 as LastUpdateTick union all
select 694 as Id, '28.3' as Code, 'Производство паровых котлов, кроме котлов центрального отопления;  производство ядерных реакторов' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 695 as Id, '28.30' as Code, 'Производство паровых котлов, кроме котлов центрального отопления;  производство ядерных реакторов' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 696 as Id, '28.30.1' as Code, 'Производство паровых котлов, кроме котлов центрального отопления; их составных частей' as Name, 695 ParentId, 0 as LastUpdateTick union all
select 697 as Id, '28.30.2' as Code, 'Производство ядерных реакторов и их составных частей' as Name, 695 ParentId, 0 as LastUpdateTick union all
select 698 as Id, '28.30.9' as Code, 'Предоставление услуг по монтажу, техническому обслуживанию и ремонту ядерных реакторов и паровых котлов, кроме котлов центрального отопления' as Name, 695 ParentId, 0 as LastUpdateTick union all
select 699 as Id, '28.4' as Code, 'Ковка, прессование, штамповка и профилирование; изготовление изделий  методом порошковой металлургии' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 700 as Id, '28.40' as Code, 'Ковка, прессование, штамповка и профилирование; изготовление изделий  методом порошковой металлургии' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 701 as Id, '28.40.1' as Code, 'Предоставление услуг по ковке, прессованию, объемной и листовой  штамповке и профилированию листового металла' as Name, 700 ParentId, 0 as LastUpdateTick union all
select 702 as Id, '28.40.2' as Code, 'Предоставление услуг по производству изделий методом порошковой  металлургии' as Name, 700 ParentId, 0 as LastUpdateTick union all
select 703 as Id, '28.5' as Code, 'Обработка металлов и нанесение покрытий на металлы; обработка  металлических изделий с использованием основных технологических процессов  машиностроения' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 704 as Id, '28.51' as Code, 'Обработка металлов и нанесение покрытий на металлы' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 705 as Id, '28.52' as Code, 'Обработка металлических изделий с использованием основных технологических процессов машиностроения' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 706 as Id, '28.6' as Code, 'Производство ножевых изделий, столовых приборов, инструментов, замочных  и скобяных изделий' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 707 as Id, '28.61' as Code, 'Производство ножевых изделий и столовых приборов' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 708 as Id, '28.62' as Code, 'Производство инструментов' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 709 as Id, '28.63' as Code, 'Производство замков и петель' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 710 as Id, '28.7' as Code, 'Производство прочих готовых металлических изделий' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 711 as Id, '28.71' as Code, 'Производство металлических бочек и аналогичных емкостей' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 712 as Id, '28.72' as Code, 'Производство упаковки из легких металлов' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 713 as Id, '28.73' as Code, 'Производство изделий из проволоки' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 714 as Id, '28.74' as Code, 'Производство крепежных изделий, цепей и пружин' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 715 as Id, '28.74.1' as Code, 'Производство крепежных изделий и пружин' as Name, 714 ParentId, 0 as LastUpdateTick union all
select 716 as Id, '28.74.2' as Code, 'Производство цепей, кроме шарнирных, и составных частей к ним' as Name, 714 ParentId, 0 as LastUpdateTick union all
select 717 as Id, '28.75' as Code, 'Производство прочих готовых металлических изделий' as Name, 684 ParentId, 0 as LastUpdateTick union all
select 718 as Id, '28.75.1' as Code, 'Производство металлических изделий для ванных комнат и кухни' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 719 as Id, '28.75.11' as Code, 'Производство раковин, моек, ванн и прочих санитарно-технических  изделий и их составных частей из черных металлов, меди или алюминия' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 720 as Id, '28.75.12' as Code, 'Производство столовых, кухонных и прочих бытовых изделий, кроме  столовых и кухонных приборов, и их составных частей из черных металлов, меди  или алюминия' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 721 as Id, '28.75.2' as Code, 'Производство прочих металлических изделий, кроме сабель, штыков и  аналогичного оружия' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 722 as Id, '28.75.21' as Code, 'Производство бронированных или армированных сейфов, несгораемых  шкафов и дверей' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 723 as Id, '28.75.22' as Code, 'Производство канцелярского настольного оборудования (ящиков, картотек,  лотков и т.п.) из недрагоценных металлов' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 724 as Id, '28.75.23' as Code, 'Производство деталей для скоросшивателей или папок; канцелярских  принадлежностей и скоб в виде полос из недрагоценных металлов' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 725 as Id, '28.75.24' as Code, 'Производство статуэток, рам для фотографий, картин, зеркал и прочих декоративных изделий из недрагоценных металлов' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 726 as Id, '28.75.25' as Code, 'Производство фурнитуры из недрагоценных металлов для одежды, обуви,  кожгалантереи и прочих изделий, в том числе крючков, пряжек, застежек, петелек,  колечек, трубчатых и раздвоенных заклепок и др.' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 727 as Id, '28.75.26' as Code, 'Производство гребных винтов и их лопастей для судовых двигателей и  лодочных моторов' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 728 as Id, '28.75.27' as Code, 'Производство прочих изделий из недрагоценных металлов, не включенных  в другие группировки' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 729 as Id, '28.75.3' as Code, 'Производство шпаг, кортиков, штыков, копий и аналогичного оружия и  частей к нему' as Name, 717 ParentId, 0 as LastUpdateTick union all
select 730 as Id, '29' as Code, 'Производство машин и оборудования' as Name, 116 ParentId, 0 as LastUpdateTick union all
select 731 as Id, '29.1' as Code, 'Производство механического оборудования' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 732 as Id, '29.11' as Code, 'Производство двигателей и турбин, кроме авиационных, ракетных, автомобильных и мотоциклетных двигателей' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 733 as Id, '29.11.1' as Code, 'Производство двигателей, кроме авиационных, ракетных, автомобильных и мотоциклетных' as Name, 732 ParentId, 0 as LastUpdateTick union all
select 734 as Id, '29.11.2' as Code, 'Производство турбин' as Name, 732 ParentId, 0 as LastUpdateTick union all
select 735 as Id, '29.11.21' as Code, 'Производство паровых турбин' as Name, 732 ParentId, 0 as LastUpdateTick union all
select 736 as Id, '29.11.22' as Code, 'Производство гидравлических турбин и водяных колес' as Name, 732 ParentId, 0 as LastUpdateTick union all
select 737 as Id, '29.11.23' as Code, 'Производство газовых турбин, кроме турбореактивных и турбовинтовых' as Name, 732 ParentId, 0 as LastUpdateTick union all
select 738 as Id, '29.11.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  двигателей и турбин, кроме авиационных, автомобильных и мотоциклетных  двигателей' as Name, 732 ParentId, 0 as LastUpdateTick union all
select 739 as Id, '29.12' as Code, 'Производство насосов, компрессоров и гидравлических систем' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 740 as Id, '29.12.1' as Code, 'Производство гидравлических и пневматических силовых установок и  двигателей' as Name, 739 ParentId, 0 as LastUpdateTick union all
select 741 as Id, '29.12.2' as Code, 'Производство насосов для перекачки жидкостей и подъемников  жидкостей' as Name, 739 ParentId, 0 as LastUpdateTick union all
select 742 as Id, '29.12.3' as Code, 'Производство воздушных и вакуумных насосов; производство воздушных и  газовых компрессоров' as Name, 739 ParentId, 0 as LastUpdateTick union all
select 743 as Id, '29.12.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  насосов и компрессоров' as Name, 739 ParentId, 0 as LastUpdateTick union all
select 744 as Id, '29.13' as Code, 'Производство трубопроводной арматуры' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 745 as Id, '29.14' as Code, 'Производство подшипников, зубчатых передач, элементов механических  передач и приводов' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 746 as Id, '29.14.1' as Code, 'Производство шариковых и роликовых подшипников' as Name, 745 ParentId, 0 as LastUpdateTick union all
select 747 as Id, '29.14.2' as Code, 'Производство корпусов подшипников и подшипников скольжения, зубчатых  колес, зубчатых передач и элементов приводов' as Name, 745 ParentId, 0 as LastUpdateTick union all
select 748 as Id, '29.14.9' as Code, 'Предоставление услуг по ремонту подшипников' as Name, 745 ParentId, 0 as LastUpdateTick union all
select 749 as Id, '29.2' as Code, 'Производство прочего оборудования общего назначения' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 750 as Id, '29.21' as Code, 'Производство печей и печных горелок' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 751 as Id, '29.21.1' as Code, 'Производство неэлектрических печей, горелок и устройств для них' as Name, 750 ParentId, 0 as LastUpdateTick union all
select 752 as Id, '29.21.2' as Code, 'Производство электрических печей' as Name, 750 ParentId, 0 as LastUpdateTick union all
select 753 as Id, '29.21.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  печей и печных топок' as Name, 750 ParentId, 0 as LastUpdateTick union all
select 754 as Id, '29.22' as Code, 'Производство подъемно-транспортного оборудования' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 755 as Id, '29.22.1' as Code, 'Производство кранов, кроме строительных' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 756 as Id, '29.22.2' as Code, 'Производство кранов для строительства' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 757 as Id, '29.22.3' as Code, 'Производство оборудования непрерывного транспорта' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 758 as Id, '29.22.4' as Code, 'Производство лифтов' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 759 as Id, '29.22.5' as Code, 'Производство авто- и электропогрузчиков' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 760 as Id, '29.22.6' as Code, 'Производство прочего подъемно-транспортного оборудования' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 761 as Id, '29.22.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию подъемно-транспортного оборудования' as Name, 754 ParentId, 0 as LastUpdateTick union all
select 762 as Id, '29.23' as Code, 'Производство промышленного холодильного и вентиляционного  оборудования' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 763 as Id, '29.23.1' as Code, 'Производство теплообменных устройств, промышленного холодильного  оборудования и оборудования для кондиционирования воздуха; производство  оборудования для фильтрования и очистки газов' as Name, 762 ParentId, 0 as LastUpdateTick union all
select 764 as Id, '29.23.2' as Code, 'Производство вентиляторов' as Name, 762 ParentId, 0 as LastUpdateTick union all
select 765 as Id, '29.23.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  промышленного холодильного и вентиляционного оборудования' as Name, 762 ParentId, 0 as LastUpdateTick union all
select 766 as Id, '29.24' as Code, 'Производство прочих машин и оборудования общего назначения, не  включенных в другие группировки' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 767 as Id, '29.24.1' as Code, 'Производство газогенераторов, аппаратов для дистилляции, фильтрования  или очистки жидкости и газов' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 768 as Id, '29.24.2' as Code, 'Производство фасовочно-упаковочного и весоизмерительного  оборудования; производство оборудования для разбрызгивания или распыления  жидких или порошкообразных материалов' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 769 as Id, '29.24.3' as Code, 'Производство центрифуг, каландров и торговых автоматов' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 770 as Id, '29.24.31' as Code, 'Производство центрифуг' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 771 as Id, '29.24.32' as Code, 'Производство каландров и прочих валковых (роликовых) машин, кроме  валковых (роликовых) машин для обработки металла и стекла' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 772 as Id, '29.24.33' as Code, 'Производство торговых автоматов, включая автоматы для размена  денег' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 773 as Id, '29.24.4' as Code, 'Производство оборудования, не включенного в другие группировки, для  обработки веществ с использованием процессов, предусматривающих изменение  температуры среды' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 774 as Id, '29.24.6' as Code, 'Производство посудомоечных машин для предприятий общественного  питания' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 775 as Id, '29.24.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  прочего оборудования общего назначения, не включенного в другие группировки' as Name, 766 ParentId, 0 as LastUpdateTick union all
select 776 as Id, '29.3' as Code, 'Производство машин и оборудования для сельского и лесного хозяйства' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 777 as Id, '29.31' as Code, 'Производство тракторов для сельского хозяйства' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 778 as Id, '29.32' as Code, 'Производство прочих машин и оборудования для сельского и лесного  хозяйства' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 779 as Id, '29.32.1' as Code, 'Производство машин, используемых в растениеводстве' as Name, 778 ParentId, 0 as LastUpdateTick union all
select 780 as Id, '29.32.2' as Code, 'Производство машин для животноводства' as Name, 778 ParentId, 0 as LastUpdateTick union all
select 781 as Id, '29.32.3' as Code, 'Производство машин для лесного хозяйства' as Name, 778 ParentId, 0 as LastUpdateTick union all
select 782 as Id, '29.32.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  машин для сельского хозяйства, включая колесные тракторы, и лесного хозяйства' as Name, 778 ParentId, 0 as LastUpdateTick union all
select 783 as Id, '29.4' as Code, 'Производство станков' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 784 as Id, '29.40' as Code, 'Производство станков' as Name, 783 ParentId, 0 as LastUpdateTick union all
select 785 as Id, '29.40.1' as Code, 'Производство металлорежущих станков' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 786 as Id, '29.40.2' as Code, 'Производство деревообрабатывающего оборудования' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 787 as Id, '29.40.3' as Code, 'Производство кузнечно-прессового оборудования' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 788 as Id, '29.40.4' as Code, 'Производство оборудования для пайки, сварки и резки, машин и аппаратов для поверхностной термообработки и газотермического напыления' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 789 as Id, '29.40.5' as Code, 'Производство станков для обработки прочих материалов' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 790 as Id, '29.40.6' as Code, 'Производство пневматического или механизированного ручного инструмента (ручных машин)' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 791 as Id, '29.40.7' as Code, 'Производство частей и принадлежностей для станков' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 792 as Id, '29.40.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию станков' as Name, 784 ParentId, 0 as LastUpdateTick union all
select 793 as Id, '29.5' as Code, 'Производство прочих машин и оборудования специального назначения' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 794 as Id, '29.51' as Code, 'Производство машин и оборудования для металлургии' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 795 as Id, '29.52' as Code, 'Производство машин и оборудования для добычи полезных ископаемых и строительства' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 796 as Id, '29.53' as Code, 'Производство машин и оборудования для изготовления пищевых продуктов,  включая напитки, и табачных изделий' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 797 as Id, '29.54' as Code, 'Производство машин и оборудования для изготовления текстильных,  швейных, меховых и кожаных изделий' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 798 as Id, '29.54.1' as Code, 'Производство оборудования для подготовки текстильных волокон,  прядения, ткачества и вязания текстильных изделий' as Name, 797 ParentId, 0 as LastUpdateTick union all
select 799 as Id, '29.54.2' as Code, 'Производство прочего оборудования для текстильной и швейной  промышленности, в том числе промышленных швейных машин' as Name, 797 ParentId, 0 as LastUpdateTick union all
select 800 as Id, '29.54.3' as Code, 'Производство машин для подготовки, дубления и выделки шкур и кожи, для  изготовления и ремонта обуви и прочих изделий из шкур и кожи, кроме швейных  машин' as Name, 797 ParentId, 0 as LastUpdateTick union all
select 801 as Id, '29.54.4' as Code, 'Производство составных частей и приспособлений машин для текстильной,  швейной и кожевенной промышленности' as Name, 797 ParentId, 0 as LastUpdateTick union all
select 802 as Id, '29.54.5' as Code, 'Производство бытовых швейных машин' as Name, 797 ParentId, 0 as LastUpdateTick union all
select 803 as Id, '29.54.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  машин для текстильной, швейной и кожевенной промышленности' as Name, 797 ParentId, 0 as LastUpdateTick union all
select 804 as Id, '29.55' as Code, 'Производство машин и оборудования для изготовления бумаги и картона' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 805 as Id, '29.56' as Code, 'Производство прочих машин и оборудования специального назначения, не  включенных в другие группировки' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 806 as Id, '29.56.1' as Code, 'Производство переплетного, наборного, включая фотонаборные машины,  печатного оборудования и его составных частей' as Name, 805 ParentId, 0 as LastUpdateTick union all
select 807 as Id, '29.56.2' as Code, 'Производство разных машин и оборудования специального назначения и их составных частей' as Name, 805 ParentId, 0 as LastUpdateTick union all
select 808 as Id, '29.56.9' as Code, 'Предоставление услуг по монтажу, техническому обслуживанию и ремонту прочих машин и оборудования специального назначения, не включенных в другие группировки' as Name, 805 ParentId, 0 as LastUpdateTick union all
select 809 as Id, '29.6' as Code, 'Производство оружия и боеприпасов' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 810 as Id, '29.60' as Code, 'Производство оружия и боеприпасов' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 811 as Id, '29.7' as Code, 'Производство бытовых приборов, не включенных в другие группировки' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 812 as Id, '29.71' as Code, 'Производство бытовых электрических приборов' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 813 as Id, '29.72' as Code, 'Производство бытовых неэлектрических приборов' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 814 as Id, '30' as Code, 'Производство офисного оборудования и вычислительной техники' as Name, 117 ParentId, 0 as LastUpdateTick union all
select 815 as Id, '30.0' as Code, 'Производство офисного оборудования и вычислительной техники' as Name, 814 ParentId, 0 as LastUpdateTick union all
select 816 as Id, '30.01' as Code, 'Производство офисного оборудования' as Name, 814 ParentId, 0 as LastUpdateTick union all
select 817 as Id, '30.01.1' as Code, 'Производство пишущих машин, машин для обработки текста,  калькуляторов, счетных машин и их частей' as Name, 816 ParentId, 0 as LastUpdateTick union all
select 818 as Id, '30.01.2' as Code, 'Производство фотокопировальных машин, офисных машин для офсетной  печати и прочих офисных машин и оборудования и их составных частей' as Name, 816 ParentId, 0 as LastUpdateTick union all
select 819 as Id, '30.01.9' as Code, 'Предоставление услуг по установке офисного оборудования' as Name, 816 ParentId, 0 as LastUpdateTick union all
select 820 as Id, '30.02' as Code, 'Производство электронных вычислительных машин и прочего оборудования для обработки информации' as Name, 814 ParentId, 0 as LastUpdateTick union all
select 821 as Id, '31' as Code, 'Производство электрических машин и электрооборудования' as Name, 117 ParentId, 0 as LastUpdateTick union all
select 822 as Id, '31.1' as Code, 'Производство электродвигателей, генераторов и трансформаторов' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 823 as Id, '31.10' as Code, 'Производство электродвигателей, генераторов и трансформаторов' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 824 as Id, '31.10.1' as Code, 'Производство электродвигателей, генераторов и трансформаторов, кроме  ремонта' as Name, 823 ParentId, 0 as LastUpdateTick union all
select 825 as Id, '31.10.9' as Code, 'Предоставление услуг по монтажу, ремонту, техническому обслуживанию и  перемотке электродвигателей, генераторов и трансформаторов' as Name, 823 ParentId, 0 as LastUpdateTick union all
select 826 as Id, '31.2' as Code, 'Производство электрической распределительной и регулирующей  аппаратуры' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 827 as Id, '31.20' as Code, 'Производство электрической распределительной и регулирующей  аппаратуры' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 828 as Id, '31.20.1' as Code, 'Производство электрической распределительной и регулирующей аппаратуры, кроме ремонта' as Name, 827 ParentId, 0 as LastUpdateTick union all
select 829 as Id, '31.20.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  электрической распределительной и регулирующей аппаратуры' as Name, 827 ParentId, 0 as LastUpdateTick union all
select 830 as Id, '31.3' as Code, 'Производство изолированных проводов и кабелей' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 831 as Id, '31.30' as Code, 'Производство изолированных проводов и кабелей' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 832 as Id, '31.4' as Code, 'Производство химических источников тока (аккумуляторов, первичных  элементов и батарей из них)' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 833 as Id, '31.40' as Code, 'Производство химических источников тока (аккумуляторов, первичных  элементов и батарей из них)' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 834 as Id, '31.40.1' as Code, 'Производство первичных элементов, батарей первичных элементов и их  частей' as Name, 833 ParentId, 0 as LastUpdateTick union all
select 835 as Id, '31.40.2' as Code, 'Производство электрических аккумуляторов, аккумуляторных батарей и их  частей' as Name, 833 ParentId, 0 as LastUpdateTick union all
select 836 as Id, '31.5' as Code, 'Производство электрических ламп и осветительного оборудования' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 837 as Id, '31.50' as Code, 'Производство электрических ламп и осветительного оборудования' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 838 as Id, '31.6' as Code, 'Производство прочего электрооборудования' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 839 as Id, '31.61' as Code, 'Производство электрооборудования для двигателей и транспортных средств' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 840 as Id, '31.62' as Code, 'Производство прочего электрооборудования, не включенного в другие  группировки, кроме электрооборудования для двигателей и транспортных средств' as Name, 821 ParentId, 0 as LastUpdateTick union all
select 841 as Id, '31.62.1' as Code, 'Производство, кроме ремонта, прочего электрооборудования, не  включенного в другие группировки, без электрооборудования для двигателей и  транспортных средств' as Name, 840 ParentId, 0 as LastUpdateTick union all
select 842 as Id, '31.62.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  прочего электрооборудования, не включенного в другие группировки' as Name, 840 ParentId, 0 as LastUpdateTick union all
select 843 as Id, '32' as Code, 'Производство электронных компонентов, аппаратуры для радио, телевидения и связи' as Name, 117 ParentId, 0 as LastUpdateTick union all
select 844 as Id, '32.1' as Code, 'Производство электро- и радиоэлементов, электровакуумных приборов' as Name, 843 ParentId, 0 as LastUpdateTick union all
select 845 as Id, '32.10' as Code, 'Производство электро- и радиоэлементов, электровакуумных приборов' as Name, 843 ParentId, 0 as LastUpdateTick union all
select 846 as Id, '32.10.1' as Code, 'Производство электрических конденсаторов, включая силовые' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 847 as Id, '32.10.2' as Code, 'Производство резисторов, включая реостаты и потенциометры' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 848 as Id, '32.10.3' as Code, 'Производство печатных схем (плат)' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 849 as Id, '32.10.4' as Code, 'Производство электровакуумных приборов' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 850 as Id, '32.10.5' as Code, 'Производство полупроводниковых элементов, приборов, включая  фоточувствительные и оптоэлектронные; смонтированных пьезоэлектрических  кристаллов' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 851 as Id, '32.10.51' as Code, 'Производство полупроводниковых приборов' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 852 as Id, '32.10.52' as Code, 'Производство смонтированных пьезоэлектрических кристаллов, включая  резонаторы, фильтры и прочие устройства' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 853 as Id, '32.10.6' as Code, 'Производство интегральных схем, микросборок и микромодулей' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 854 as Id, '32.10.7' as Code, 'Производство частей электровакуумных приборов и прочих электро- и  радиоэлементов, не включенных в другие группировки' as Name, 845 ParentId, 0 as LastUpdateTick union all
select 855 as Id, '32.2' as Code, 'Производство телевизионной и радиопередающей аппаратуры, аппаратуры электросвязи' as Name, 843 ParentId, 0 as LastUpdateTick union all
select 856 as Id, '32.20' as Code, 'Производство телевизионной и радиопередающей аппаратуры, аппаратуры электросвязи' as Name, 843 ParentId, 0 as LastUpdateTick union all
select 857 as Id, '32.20.1' as Code, 'Производство радио- и телевизионной передающей аппаратуры' as Name, 856 ParentId, 0 as LastUpdateTick union all
select 858 as Id, '32.20.2' as Code, 'Производство аппаратуры электросвязи' as Name, 856 ParentId, 0 as LastUpdateTick union all
select 859 as Id, '32.20.3' as Code, 'Производство частей радио- и телевизионной аппаратуры, аппаратуры электросвязи' as Name, 856 ParentId, 0 as LastUpdateTick union all
select 860 as Id, '32.20.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию телевизионной и радиопередающей аппаратуры, аппаратуры электросвязи, аппаратуры для передачи данных' as Name, 856 ParentId, 0 as LastUpdateTick union all
select 861 as Id, '32.3' as Code, 'Производство аппаратуры для приема, записи и воспроизведения звука и  изображения' as Name, 843 ParentId, 0 as LastUpdateTick union all
select 862 as Id, '32.30' as Code, 'Производство аппаратуры для приема, записи и воспроизведения звука и  изображения' as Name, 843 ParentId, 0 as LastUpdateTick union all
select 863 as Id, '32.30.1' as Code, 'Производство радиоприемников' as Name, 862 ParentId, 0 as LastUpdateTick union all
select 864 as Id, '32.30.2' as Code, 'Производство телевизионных приемников, включая видеомониторы и  видеопроекторы' as Name, 862 ParentId, 0 as LastUpdateTick union all
select 865 as Id, '32.30.3' as Code, 'Производство звукозаписывающей и звуковоспроизводящей аппаратуры и аппаратуры для записи и воспроизведения изображений' as Name, 862 ParentId, 0 as LastUpdateTick union all
select 866 as Id, '32.30.4' as Code, 'Производство электроакустической аппаратуры' as Name, 862 ParentId, 0 as LastUpdateTick union all
select 867 as Id, '32.30.5' as Code, 'Производство частей звукозаписывающей и звуковоспроизводящей аппаратуры и видеоаппаратуры; антенн' as Name, 862 ParentId, 0 as LastUpdateTick union all
select 868 as Id, '32.30.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию профессиональной радио-, телевизионной, звукозаписывающей и звуковоспроизводящей аппаратуры и видеоаппаратуры' as Name, 862 ParentId, 0 as LastUpdateTick union all
select 869 as Id, '33' as Code, 'Производство медицинских изделий; средств измерений, контроля, управления и испытаний; оптических приборов, фото- и кинооборудования; часов' as Name, 117 ParentId, 0 as LastUpdateTick union all
select 870 as Id, '33.1' as Code, 'Производство медицинских изделий, включая хирургическое оборудование, и ортопедических приспособлений' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 871 as Id, '33.10' as Code, 'Производство медицинских изделий, включая хирургическое оборудование, и ортопедических приспособлений' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 872 as Id, '33.10.1' as Code, 'Производство аппаратуры, основанной на использовании рентгеновского, альфа-, бета- и гамма-излучений; медицинского инструмента, оборудования и приспособлений; диагностической и терапевтической аппаратуры; специализированных средств защиты; их составных частей' as Name, 871 ParentId, 0 as LastUpdateTick union all
select 873 as Id, '33.10.2' as Code, 'Производство медицинской мебели, в том числе хирургической, стоматологической и ветеринарной; производство стоматологических и аналогичных им кресел с устройствами для поворота, подъема и наклона и их составных частей' as Name, 871 ParentId, 0 as LastUpdateTick union all
select 874 as Id, '33.10.9' as Code, 'Предоставление услуг по монтажу, техническому обслуживанию и ремонту медицинского оборудования и аппаратуры' as Name, 871 ParentId, 0 as LastUpdateTick union all
select 875 as Id, '33.2' as Code, 'Производство приборов и инструментов для измерений, контроля, испытаний, навигации, управления и прочих целей' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 876 as Id, '33.20' as Code, 'Производство приборов и инструментов для измерений, контроля, испытаний, навигации, управления и прочих целей' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 877 as Id, '33.20.1' as Code, 'Производство навигационных, метеорологических, геодезических, геофизических и аналогичного типа приборов, аппаратуры и инструментов' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 878 as Id, '33.20.2' as Code, 'Производство радиолокационной, радионавигационной аппаратуры и радиоаппаратуры дистанционного управления' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 879 as Id, '33.20.3' as Code, 'Производство точных весов; производство ручных инструментов для  черчения, разметки и математических расчетов; производство ручных инструментов  для измерения линейных размеров, не включенных в другие группировки' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 880 as Id, '33.20.4' as Code, 'Производство приборов и аппаратуры для контроля и измерения электрических величин, ионизирующих излучений и параметров электросвязи' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 881 as Id, '33.20.5' as Code, 'Производство приборов и аппаратуры для контроля прочих физических величин' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 882 as Id, '33.20.6' as Code, 'Производство прочих приборов, аппаратуры и инструментов для измерения, контроля и испытаний' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 883 as Id, '33.20.7' as Code, 'Производство приборов и аппаратуры для автоматического регулирования или управления' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 884 as Id, '33.20.8' as Code, 'Производство частей приборов и инструментов для навигации, управления, измерения, контроля, испытаний и прочих целей' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 885 as Id, '33.20.9' as Code, 'Предоставление услуг по монтажу, техническому обслуживанию и ремонту приборов и инструментов для измерений, контроля, испытаний, навигации, управления и прочих целей' as Name, 876 ParentId, 0 as LastUpdateTick union all
select 886 as Id, '33.3' as Code, 'Производство приборов контроля и регулирования технологических процессов' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 887 as Id, '33.30' as Code, 'Производство приборов контроля и регулирования технологических процессов' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 888 as Id, '33.4' as Code, 'Производство оптических приборов, фото- и кинооборудования' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 889 as Id, '33.40' as Code, 'Производство оптических приборов, фото- и кинооборудования' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 890 as Id, '33.40.1' as Code, 'Производство оптических приборов, фото- и кинооборудования, кроме ремонта' as Name, 889 ParentId, 0 as LastUpdateTick union all
select 891 as Id, '33.40.9' as Code, 'Предоставление услуг по ремонту и техническому обслуживанию  профессионального фото- и кинооборудования и оптических приборов' as Name, 889 ParentId, 0 as LastUpdateTick union all
select 892 as Id, '33.5' as Code, 'Производство часов и других приборов времени' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 893 as Id, '33.50' as Code, 'Производство часов и других приборов времени' as Name, 869 ParentId, 0 as LastUpdateTick union all
select 894 as Id, '33.50.1' as Code, 'Производство готовых часов и других приборов времени' as Name, 893 ParentId, 0 as LastUpdateTick union all
select 895 as Id, '33.50.2' as Code, 'Производство часовых механизмов и частей часов и приборов времени' as Name, 893 ParentId, 0 as LastUpdateTick union all
select 896 as Id, '33.50.9' as Code, 'Предоставление услуг по монтажу, ремонту и техническому обслуживанию  промышленных приборов и аппаратуры для измерения временных интервалов' as Name, 893 ParentId, 0 as LastUpdateTick union all
select 897 as Id, '34' as Code, 'Производство автомобилей, прицепов и полуприцепов' as Name, 118 ParentId, 0 as LastUpdateTick union all
select 898 as Id, '34.1' as Code, 'Производство автомобилей' as Name, 897 ParentId, 0 as LastUpdateTick union all
select 899 as Id, '34.10' as Code, 'Производство автомобилей' as Name, 897 ParentId, 0 as LastUpdateTick union all
select 900 as Id, '34.10.1' as Code, 'Производство двигателей внутреннего сгорания для автомобилей' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 901 as Id, '34.10.2' as Code, 'Производство легковых автомобилей' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 902 as Id, '34.10.3' as Code, 'Производство автобусов и троллейбусов' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 903 as Id, '34.10.4' as Code, 'Производство грузовых автомобилей' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 904 as Id, '34.10.5' as Code, 'Производство автомобилей специального назначения' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 905 as Id, '34.2' as Code, 'Производство автомобильных кузовов; производство прицепов, полуприцепов  и контейнеров, предназначенных для перевозки одним или несколькими видами  транспорта' as Name, 897 ParentId, 0 as LastUpdateTick union all
select 906 as Id, '34.20' as Code, 'Производство автомобильных кузовов; производство прицепов, полуприцепов  и контейнеров, предназначенных для перевозки одним или несколькими видами  транспорта' as Name, 897 ParentId, 0 as LastUpdateTick union all
select 907 as Id, '34.3' as Code, 'Производство частей и принадлежностей автомобилей и их двигателей' as Name, 897 ParentId, 0 as LastUpdateTick union all
select 908 as Id, '34.30' as Code, 'Производство частей и принадлежностей автомобилей и их двигателей' as Name, 897 ParentId, 0 as LastUpdateTick union all
select 909 as Id, '35' as Code, 'Производство судов, летательных и космических аппаратов и прочих транспортных средств' as Name, 118 ParentId, 0 as LastUpdateTick union all
select 910 as Id, '35.1' as Code, 'Строительство и ремонт судов' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 911 as Id, '35.11' as Code, 'Строительство и ремонт судов' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 912 as Id, '35.11.1' as Code, 'Строительство судов' as Name, 911 ParentId, 0 as LastUpdateTick union all
select 913 as Id, '35.11.9' as Code, 'Предоставление услуг по ремонту, техническому обслуживанию судов и переоборудованию судов' as Name, 911 ParentId, 0 as LastUpdateTick union all
select 914 as Id, '35.12' as Code, 'Строительство и ремонт спортивных и туристских судов' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 915 as Id, '35.12.1' as Code, 'Строительство спортивных и туристских (прогулочных) судов' as Name, 914 ParentId, 0 as LastUpdateTick union all
select 916 as Id, '35.12.9' as Code, 'Предоставление услуг по ремонту, техническому обслуживанию и переоборудованию спортивных и туристских (прогулочных) судов' as Name, 914 ParentId, 0 as LastUpdateTick union all
select 917 as Id, '35.2' as Code, 'Производство железнодорожного подвижного состава (локомотивов,  трамвайных моторных вагонов и прочего подвижного состава)' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 918 as Id, '35.20' as Code, 'Производство железнодорожного подвижного состава (локомотивов,  трамвайных моторных вагонов и прочего подвижного состава)' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 919 as Id, '35.20.1' as Code, 'Производство железнодорожных локомотивов' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 920 as Id, '35.20.2' as Code, 'Производство моторных железнодорожных, трамвайных вагонов и вагонов  метро, автомотрис и автодрезин, кроме транспортных средств для ремонта и  технического обслуживания железнодорожных и трамвайных путей' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 921 as Id, '35.20.3' as Code, 'Производство прочего подвижного состава' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 922 as Id, '35.20.31' as Code, 'Производство транспортных средств для ремонта и технического  обслуживания железнодорожных, трамвайных и прочих путей' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 923 as Id, '35.20.32' as Code, 'Производство несамоходных пассажирских железнодорожных, трамвайных  вагонов и вагонов метро, багажных, почтовых и прочих вагонов специального  назначения, кроме вагонов, предназначенных для ремонта и технического  обслуживания путей' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 924 as Id, '35.20.33' as Code, 'Производство несамоходных железнодорожных, трамвайных и прочих  вагонов для перевозки грузов' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 925 as Id, '35.20.4' as Code, 'Производство частей железнодорожных локомотивов, трамвайных и прочих  моторных вагонов и подвижного состава; производство путевого оборудования и устройств для железнодорожных, трамвайных и прочих путей, механического и электромеханического оборудования для управления движением' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 926 as Id, '35.20.9' as Code, 'Предоставление услуг по ремонту, техническому обслуживанию и  переделке железнодорожных локомотивов, трамвайных и прочих моторных вагонов  и подвижного состава' as Name, 918 ParentId, 0 as LastUpdateTick union all
select 927 as Id, '35.3' as Code, 'Производство летательных аппаратов, включая космические' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 928 as Id, '35.30' as Code, 'Производство летательных аппаратов, включая космические' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 929 as Id, '35.30.1' as Code, 'Производство силовых установок и двигателей для летательных аппаратов, включая космические; устройств для ускоренного взлета самолетов, палубных тормозных устройств; наземных тренажеров для летного состава; их частей' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 930 as Id, '35.30.11' as Code, 'Производство двигателей летательных аппаратов с искровым зажиганием  и их частей' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 931 as Id, '35.30.12' as Code, 'Производство турбореактивных и турбовинтовых двигателей и их частей' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 932 as Id, '35.30.13' as Code, 'Производство реактивных двигателей, включая ракетные, и их частей' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 933 as Id, '35.30.14' as Code, 'Производство устройств для ускоренного взлета самолетов, палубных тормозных устройств и аналогичных устройств' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 934 as Id, '35.30.17' as Code, 'Производство наземных тренажеров для летного состава и их частей' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 935 as Id, '35.30.2' as Code, 'Производство воздушных шаров, дирижаблей, планеров, дельтапланов и  прочих безмоторных летательных аппаратов' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 936 as Id, '35.30.3' as Code, 'Производство вертолетов, самолетов и прочих летательных аппаратов' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 937 as Id, '35.30.4' as Code, 'Производство космических аппаратов, ракет-носителей и прочих космических объектов' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 938 as Id, '35.30.41' as Code, 'Производство автоматических космических аппаратов' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 939 as Id, '35.30.42' as Code, 'Производство пилотируемых и беспилотных космических кораблей и станций, включая орбитальные, межпланетные, многоразового использования и др.' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 940 as Id, '35.30.43' as Code, 'Производство средств выведения космических объектов в космическое пространство' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 941 as Id, '35.30.5' as Code, 'Производство прочих частей и принадлежностей летательных аппаратов, включая части космических объектов и средств их выведения в космическое пространство' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 942 as Id, '35.30.9' as Code, 'Предоставление услуг по монтажу, техническому обслуживанию, ремонту и восстановлению летательных аппаратов и двигателей летательных аппаратов, включая составные части ракет космического назначения' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 943 as Id, '35.4' as Code, 'Производство мотоциклов и велосипедов' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 944 as Id, '35.41' as Code, 'Производство мотоциклов, мопедов и мотоциклетных колясок' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 945 as Id, '35.42' as Code, 'Производство велосипедов' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 946 as Id, '35.43' as Code, 'Производство инвалидных колясок' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 947 as Id, '35.5' as Code, 'Производство прочих транспортных средств и оборудования, не включенных в  другие группировки' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 948 as Id, '35.50' as Code, 'Производство прочих транспортных средств и оборудования, не включенных  в другие группировки' as Name, 909 ParentId, 0 as LastUpdateTick union all
select 949 as Id, '36' as Code, 'Производство мебели и прочей продукции, не включенной в другие группировки' as Name, 119 ParentId, 0 as LastUpdateTick union all
select 950 as Id, '36.1' as Code, 'Производство мебели' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 951 as Id, '36.11' as Code, 'Производство стульев и другой мебели для сидения' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 952 as Id, '36.12' as Code, 'Производство мебели для офисов и предприятий торговли' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 953 as Id, '36.13' as Code, 'Производство кухонной мебели' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 954 as Id, '36.14' as Code, 'Производство прочей мебели' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 955 as Id, '36.15' as Code, 'Производство матрасов' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 956 as Id, '36.2' as Code, 'Производство ювелирных изделий, медалей и технических изделий из драгоценных металлов и драгоценных камней; производство монет' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 957 as Id, '36.21' as Code, 'Чеканка монет' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 958 as Id, '36.22' as Code, 'Производство ювелирных изделий, медалей и технических изделий из драгоценных металлов и драгоценных камней' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 959 as Id, '36.22.1' as Code, 'Производство изделий технического назначения из драгоценных металлов' as Name, 958 ParentId, 0 as LastUpdateTick union all
select 960 as Id, '36.22.2' as Code, 'Производство изделий технического назначения из драгоценных камней' as Name, 958 ParentId, 0 as LastUpdateTick union all
select 961 as Id, '36.22.3' as Code, 'Обработка алмазов' as Name, 958 ParentId, 0 as LastUpdateTick union all
select 962 as Id, '36.22.4' as Code, 'Обработка драгоценных, кроме алмазов, полудрагоценных, поделочных и  синтетических камней' as Name, 958 ParentId, 0 as LastUpdateTick union all
select 963 as Id, '36.22.5' as Code, 'Производство ювелирных изделий, медалей из драгоценных металлов и драгоценных камней' as Name, 958 ParentId, 0 as LastUpdateTick union all
select 964 as Id, '36.3' as Code, 'Производство музыкальных инструментов' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 965 as Id, '36.30' as Code, 'Производство музыкальных инструментов' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 966 as Id, '36.4' as Code, 'Производство спортивных товаров' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 967 as Id, '36.40' as Code, 'Производство спортивных товаров' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 968 as Id, '36.5' as Code, 'Производство игр и игрушек' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 969 as Id, '36.50' as Code, 'Производство игр и игрушек' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 970 as Id, '36.6' as Code, 'Производство различной продукции, не включенной в другие группировки' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 971 as Id, '36.61' as Code, 'Производство ювелирных изделий из недрагоценных материалов' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 972 as Id, '36.62' as Code, 'Производство метел и щеток' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 973 as Id, '36.63' as Code, 'Производство прочей продукции, не включенной в другие группировки' as Name, 949 ParentId, 0 as LastUpdateTick union all
select 974 as Id, '36.63.1' as Code, 'Производство каруселей, качелей, тиров и прочих ярмарочных  аттракционов' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 975 as Id, '36.63.2' as Code, 'Производство пишущих принадлежностей' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 976 as Id, '36.63.3' as Code, 'Производство зонтов, тростей, пуговиц, кнопок, застежек-молний' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 977 as Id, '36.63.4' as Code, 'Производство линолеума на текстильной основе' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 978 as Id, '36.63.5' as Code, 'Производство изделий из волоса человека или животных; производство  аналогичных изделий из текстильных материалов' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 979 as Id, '36.63.6' as Code, 'Производство спичек и зажигалок' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 980 as Id, '36.63.7' as Code, 'Производство прочих изделий, не включенных в другие группировки' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 981 as Id, '37' as Code, 'Обработка вторичного сырья' as Name, 119 ParentId, 0 as LastUpdateTick union all
select 982 as Id, '37.1' as Code, 'Обработка металлических отходов и лома' as Name, 981 ParentId, 0 as LastUpdateTick union all
select 983 as Id, '37.10' as Code, 'Обработка металлических отходов и лома' as Name, 981 ParentId, 0 as LastUpdateTick union all
select 984 as Id, '37.10.1' as Code, 'Обработка отходов и лома черных металлов' as Name, 983 ParentId, 0 as LastUpdateTick union all
select 985 as Id, '37.10.2' as Code, 'Обработка отходов и лома цветных металлов' as Name, 983 ParentId, 0 as LastUpdateTick union all
select 986 as Id, '37.10.21' as Code, 'Обработка отходов и лома цветных металлов, кроме драгоценных' as Name, 983 ParentId, 0 as LastUpdateTick union all
select 987 as Id, '37.10.22' as Code, 'Обработка отходов и лома драгоценных металлов' as Name, 983 ParentId, 0 as LastUpdateTick union all
select 988 as Id, '37.2' as Code, 'Обработка неметаллических отходов и лома' as Name, 981 ParentId, 0 as LastUpdateTick union all
select 989 as Id, '37.20' as Code, 'Обработка неметаллических отходов и лома' as Name, 981 ParentId, 0 as LastUpdateTick union all
select 990 as Id, '37.20.1' as Code, 'Обработка отходов резины' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 991 as Id, '37.20.2' as Code, 'Обработка отходов и лома пластмасс' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 992 as Id, '37.20.3' as Code, 'Обработка отходов и лома стекла' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 993 as Id, '37.20.4' as Code, 'Обработка отходов текстильных материалов' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 994 as Id, '37.20.5' as Code, 'Обработка отходов бумаги и картона' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 995 as Id, '37.20.6' as Code, 'Обработка отходов драгоценных камней' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 996 as Id, '37.20.7' as Code, 'Обработка прочих неметаллических отходов и лома' as Name, 989 ParentId, 0 as LastUpdateTick union all
select 997 as Id, '40' as Code, 'Производство, передача и распределение электроэнергии, газа, пара и горячей воды' as Name, 2424 ParentId, 0 as LastUpdateTick union all
select 998 as Id, '40.1' as Code, 'Производство, передача и распределение электроэнергии' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 999 as Id, '40.10' as Code, 'Производство, передача и распределение электроэнергии' as Name, 998 ParentId, 0 as LastUpdateTick union all
select 1000 as Id, '40.10.1' as Code, 'Производство электроэнергии' as Name, 999 ParentId, 0 as LastUpdateTick union all
select 1001 as Id, '40.10.11' as Code, 'Производство электроэнергии тепловыми электростанциями' as Name, 1000 ParentId, 0 as LastUpdateTick union all
select 1002 as Id, '40.10.12' as Code, 'Производство электроэнергии гидроэлектростанциями' as Name, 1000 ParentId, 0 as LastUpdateTick union all
select 1003 as Id, '40.10.13' as Code, 'Производство электроэнергии атомными электростанциями' as Name, 1000 ParentId, 0 as LastUpdateTick union all
select 1004 as Id, '40.10.14' as Code, 'Производство электроэнергии прочими электростанциями и промышленными блок-станциями' as Name, 1000 ParentId, 0 as LastUpdateTick union all
select 1005 as Id, '40.10.2' as Code, 'Передача электроэнергии' as Name, 999 ParentId, 0 as LastUpdateTick union all
select 1006 as Id, '40.10.3' as Code, 'Распределение электроэнергии' as Name, 999 ParentId, 0 as LastUpdateTick union all
select 1007 as Id, '40.10.4' as Code, 'Деятельность по обеспечению работоспособности электростанций' as Name, 999 ParentId, 0 as LastUpdateTick union all
select 1008 as Id, '40.10.41' as Code, 'Деятельность по обеспечению работоспособности тепловых электростанций' as Name, 1007 ParentId, 0 as LastUpdateTick union all
select 1009 as Id, '40.10.42' as Code, 'Деятельность по обеспечению работоспособности гидроэлектростанций' as Name, 1007 ParentId, 0 as LastUpdateTick union all
select 1010 as Id, '40.10.43' as Code, 'Деятельность по обеспечению работоспособности атомных электростанций' as Name, 1007 ParentId, 0 as LastUpdateTick union all
select 1011 as Id, '40.10.44' as Code, 'Деятельность по обеспечению работоспособности прочих электростанций и промышленных блок-станций' as Name, 1007 ParentId, 0 as LastUpdateTick union all
select 1012 as Id, '40.10.5' as Code, 'Деятельность по обеспечению работоспособности электрических сетей' as Name, 999 ParentId, 0 as LastUpdateTick union all
select 1013 as Id, '40.2' as Code, 'Производство и распределение газообразного топлива' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 1014 as Id, '40.20' as Code, 'Производство и распределение газообразного топлива' as Name, 1013 ParentId, 0 as LastUpdateTick union all
select 1015 as Id, '40.20.1' as Code, 'Производство газообразного топлива' as Name, 1014 ParentId, 0 as LastUpdateTick union all
select 1016 as Id, '40.20.2' as Code, 'Распределение газообразного топлива' as Name, 1014 ParentId, 0 as LastUpdateTick union all
select 1017 as Id, '40.3' as Code, 'Производство, передача и распределение пара и горячей воды (тепловой  энергии)' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 1018 as Id, '40.30' as Code, 'Производство, передача и распределение пара и горячей воды (тепловой энергии)' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 1019 as Id, '40.30.1' as Code, 'Производство пара и горячей воды (тепловой энергии)' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1020 as Id, '40.30.11' as Code, 'Производство пара и горячей воды (тепловой энергии) тепловыми  электростанциями' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1021 as Id, '40.30.12' as Code, 'Производство пара и горячей воды (тепловой энергии) атомными  электростанциями' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1022 as Id, '40.30.13' as Code, 'Производство пара и горячей воды (тепловой энергии) прочими  электростанциями и промышленными блок-станциями' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1023 as Id, '40.30.14' as Code, 'Производство пара и горячей воды (тепловой энергии) котельными' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1024 as Id, '40.30.17' as Code, 'Производство охлажденной воды или льда (натурального из воды) для  охлаждения' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1025 as Id, '40.30.2' as Code, 'Передача пара и горячей воды (тепловой энергии)' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1026 as Id, '40.30.3' as Code, 'Распределение пара и горячей воды (тепловой энергии)' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1027 as Id, '40.30.4' as Code, 'Деятельность по обеспечению работоспособности котельных' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1028 as Id, '40.30.5' as Code, 'Деятельность по обеспечению работоспособности тепловых сетей' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 1029 as Id, '41' as Code, 'Сбор, очистка и распределение воды' as Name, 2424 ParentId, 0 as LastUpdateTick union all
select 1030 as Id, '41.0' as Code, 'Сбор, очистка и распределение воды' as Name, 1029 ParentId, 0 as LastUpdateTick union all
select 1031 as Id, '41.00' as Code, 'Сбор, очистка и распределение воды' as Name, 1029 ParentId, 0 as LastUpdateTick union all
select 1032 as Id, '41.00.1' as Code, 'Сбор и очистка воды' as Name, 1031 ParentId, 0 as LastUpdateTick union all
select 1033 as Id, '41.00.2' as Code, 'Распределение воды' as Name, 1031 ParentId, 0 as LastUpdateTick union all
select 1034 as Id, '45' as Code, 'Строительство' as Name, 2425 ParentId, 0 as LastUpdateTick union all
select 1035 as Id, '45.1' as Code, 'Подготовка строительного участка' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1036 as Id, '45.11' as Code, 'Разборка и снос зданий; производство земляных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1037 as Id, '45.11.1' as Code, 'Разборка и снос зданий, расчистка строительных участков' as Name, 1036 ParentId, 0 as LastUpdateTick union all
select 1038 as Id, '45.11.2' as Code, 'Производство земляных работ' as Name, 1036 ParentId, 0 as LastUpdateTick union all
select 1039 as Id, '45.11.3' as Code, 'Подготовка участка для горных работ' as Name, 1036 ParentId, 0 as LastUpdateTick union all
select 1040 as Id, '45.12' as Code, 'Разведочное бурение' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1041 as Id, '45.2' as Code, 'Строительство зданий и сооружений' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1042 as Id, '45.21' as Code, 'Производство общестроительных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1043 as Id, '45.21.1' as Code, 'Производство общестроительных работ по возведению зданий' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1044 as Id, '45.21.2' as Code, 'Производство общестроительных работ по строительству мостов, надземных автомобильных дорог, тоннелей и подземных дорог' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1045 as Id, '45.21.3' as Code, 'Производство общестроительных работ по прокладке магистральных трубопроводов, линий связи и линий электропередачи' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1046 as Id, '45.21.4' as Code, 'Производство общестроительных работ по прокладке местных трубопроводов, линий связи и линий электропередачи, включая взаимосвязанные вспомогательные работы' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1047 as Id, '45.21.5' as Code, 'Производство общестроительных работ по строительству электростанций и  сооружений для горнодобывающей и обрабатывающей промышленности' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1048 as Id, '45.21.51' as Code, 'Производство общестроительных работ по строительству гидроэлектростанций' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1049 as Id, '45.21.52' as Code, 'Производство общестроительных работ по строительству атомных электростанций' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1050 as Id, '45.21.53' as Code, 'Производство общестроительных работ по строительству тепловых и прочих электростанций' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1051 as Id, '45.21.54' as Code, 'Производство общестроительных работ по строительству сооружений для горнодобывающей и обрабатывающей промышленности' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1052 as Id, '45.21.6' as Code, 'Производство общестроительных работ по строительству прочих зданий и сооружений, не включенных в другие группировки' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1053 as Id, '45.21.7' as Code, 'Монтаж зданий и сооружений из сборных конструкций' as Name, 1042 ParentId, 0 as LastUpdateTick union all
select 1054 as Id, '45.22' as Code, 'Устройство покрытий зданий и сооружений' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1055 as Id, '45.23' as Code, 'Строительство дорог, аэродромов и спортивных сооружений' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1056 as Id, '45.23.1' as Code, 'Производство общестроительных работ по строительству автомобильных дорог, железных дорог и взлетно-посадочных полос аэродромов' as Name, 1055 ParentId, 0 as LastUpdateTick union all
select 1057 as Id, '45.23.2' as Code, 'Строительство спортивных сооружений' as Name, 1055 ParentId, 0 as LastUpdateTick union all
select 1058 as Id, '45.24' as Code, 'Строительство водных сооружений' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1059 as Id, '45.24.1' as Code, 'Строительство портовых сооружений' as Name, 1058 ParentId, 0 as LastUpdateTick union all
select 1060 as Id, '45.24.2' as Code, 'Строительство гидротехнических сооружений' as Name, 1058 ParentId, 0 as LastUpdateTick union all
select 1061 as Id, '45.24.3' as Code, 'Производство дноуглубительных и берегоукрепительных работ' as Name, 1058 ParentId, 0 as LastUpdateTick union all
select 1062 as Id, '45.24.4' as Code, 'Производство подводных работ, включая водолазные' as Name, 1058 ParentId, 0 as LastUpdateTick union all
select 1063 as Id, '45.25' as Code, 'Производство прочих строительных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1064 as Id, '45.25.1' as Code, 'Монтаж строительных лесов и подмостей' as Name, 1063 ParentId, 0 as LastUpdateTick union all
select 1065 as Id, '45.25.2' as Code, 'Строительство фундаментов и бурение водяных скважин' as Name, 1063 ParentId, 0 as LastUpdateTick union all
select 1066 as Id, '45.25.3' as Code, 'Производство бетонных и железобетонных работ' as Name, 1063 ParentId, 0 as LastUpdateTick union all
select 1067 as Id, '45.25.4' as Code, 'Монтаж металлических строительных конструкций' as Name, 1063 ParentId, 0 as LastUpdateTick union all
select 1068 as Id, '45.25.5' as Code, 'Производство каменных работ' as Name, 1063 ParentId, 0 as LastUpdateTick union all
select 1069 as Id, '45.25.6' as Code, 'Производство прочих строительных работ, требующих специальной квалификации' as Name, 1063 ParentId, 0 as LastUpdateTick union all
select 1070 as Id, '45.3' as Code, 'Монтаж инженерного оборудования зданий и сооружений' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1071 as Id, '45.31' as Code, 'Производство электромонтажных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1072 as Id, '45.32' as Code, 'Производство изоляционных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1073 as Id, '45.33' as Code, 'Производство санитарно-технических работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1074 as Id, '45.34' as Code, 'Монтаж прочего инженерного оборудования' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1075 as Id, '45.4' as Code, 'Производство отделочных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1076 as Id, '45.41' as Code, 'Производство штукатурных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1077 as Id, '45.42' as Code, 'Производство столярных и плотничных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1078 as Id, '45.43' as Code, 'Устройство покрытий полов и облицовка стен' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1079 as Id, '45.44' as Code, 'Производство малярных и стекольных работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1080 as Id, '45.44.1' as Code, 'Производство стекольных работ' as Name, 1079 ParentId, 0 as LastUpdateTick union all
select 1081 as Id, '45.44.2' as Code, 'Производство малярных работ' as Name, 1079 ParentId, 0 as LastUpdateTick union all
select 1082 as Id, '45.45' as Code, 'Производство прочих отделочных и завершающих работ' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1083 as Id, '45.5' as Code, 'Аренда строительных машин и оборудования с оператором' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1084 as Id, '45.50' as Code, 'Аренда строительных машин и оборудования с оператором' as Name, 1034 ParentId, 0 as LastUpdateTick union all
select 1085 as Id, '50' as Code, 'Торговля автотранспортными средствами и мотоциклами, их техническое обслуживание и ремонт' as Name, 2426 ParentId, 0 as LastUpdateTick union all
select 1086 as Id, '50.1' as Code, 'Торговля автотранспортными средствами' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1087 as Id, '50.10' as Code, 'Торговля автотранспортными средствами' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1088 as Id, '50.10.1' as Code, 'Оптовая торговля автотранспортными средствами' as Name, 1087 ParentId, 0 as LastUpdateTick union all
select 1089 as Id, '50.10.2' as Code, 'Розничная торговля автотранспортными средствами' as Name, 1087 ParentId, 0 as LastUpdateTick union all
select 1090 as Id, '50.10.3' as Code, 'Торговля автотранспортными средствами через агентов' as Name, 1087 ParentId, 0 as LastUpdateTick union all
select 1091 as Id, '50.2' as Code, 'Техническое обслуживание и ремонт автотранспортных средств' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1092 as Id, '50.20' as Code, 'Техническое обслуживание и ремонт автотранспортных средств' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1093 as Id, '50.20.1' as Code, 'Техническое обслуживание и ремонт легковых автомобилей' as Name, 1092 ParentId, 0 as LastUpdateTick union all
select 1094 as Id, '50.20.2' as Code, 'Техническое обслуживание и ремонт прочих автотранспортных средств' as Name, 1092 ParentId, 0 as LastUpdateTick union all
select 1095 as Id, '50.20.3' as Code, 'Предоставление прочих видов услуг по техническому обслуживанию автотранспортных средств' as Name, 1092 ParentId, 0 as LastUpdateTick union all
select 1096 as Id, '50.3' as Code, 'Торговля автомобильными деталями, узлами и принадлежностями' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1097 as Id, '50.30' as Code, 'Торговля автомобильными деталями, узлами и принадлежностями' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1098 as Id, '50.30.1' as Code, 'Оптовая торговля автомобильными деталями, узлами и принадлежностями' as Name, 1097 ParentId, 0 as LastUpdateTick union all
select 1099 as Id, '50.30.2' as Code, 'Розничная торговля автомобильными деталями, узлами и  принадлежностями' as Name, 1097 ParentId, 0 as LastUpdateTick union all
select 1100 as Id, '50.30.3' as Code, 'Торговля автомобильными деталями, узлами и принадлежностями через агентов' as Name, 1097 ParentId, 0 as LastUpdateTick union all
select 1101 as Id, '50.4' as Code, 'Торговля мотоциклами, их деталями, узлами и принадлежностями;  техническое обслуживание и ремонт мотоциклов' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1102 as Id, '50.40' as Code, 'Торговля мотоциклами, их деталями, узлами и принадлежностями; техническое обслуживание и ремонт мотоциклов' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1103 as Id, '50.40.1' as Code, 'Оптовая торговля мотоциклами, их деталями, узлами и принадлежностями' as Name, 1102 ParentId, 0 as LastUpdateTick union all
select 1104 as Id, '50.40.2' as Code, 'Розничная торговля мотоциклами, их деталями, узлами и принадлежностями' as Name, 1102 ParentId, 0 as LastUpdateTick union all
select 1105 as Id, '50.40.3' as Code, 'Торговля мотоциклами, их деталями, узлами и принадлежностями через агентов' as Name, 1102 ParentId, 0 as LastUpdateTick union all
select 1106 as Id, '50.40.4' as Code, 'Техническое обслуживание и ремонт мотоциклов' as Name, 1102 ParentId, 0 as LastUpdateTick union all
select 1107 as Id, '50.5' as Code, 'Розничная торговля моторным топливом' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1108 as Id, '50.50' as Code, 'Розничная торговля моторным топливом' as Name, 1085 ParentId, 0 as LastUpdateTick union all
select 1109 as Id, '51' as Code, 'Оптовая торговля, включая торговлю через агентов, кроме торговли автотранспортными средствами и мотоциклами' as Name, 2426 ParentId, 0 as LastUpdateTick union all
select 1110 as Id, '51.1' as Code, 'Оптовая торговля через агентов (за вознаграждение или на договорной основе)' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1111 as Id, '51.11' as Code, 'Деятельность агентов по оптовой торговле живыми животными,  сельскохозяйственным сырьем, текстильным сырьем и полуфабрикатами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1112 as Id, '51.11.1' as Code, 'Деятельность агентов по оптовой торговле живыми животными' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1113 as Id, '51.11.2' as Code, 'Деятельность агентов по оптовой торговле сельскохозяйственным сырьем,  текстильным сырьем и полуфабрикатами' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1114 as Id, '51.11.21' as Code, 'Деятельность агентов по оптовой торговле зерном' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1115 as Id, '51.11.22' as Code, 'Деятельность агентов по оптовой торговле семенами, кроме масличных' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1116 as Id, '51.11.23' as Code, 'Деятельность агентов по оптовой торговле масличными семенами и  маслосодержащими плодами' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1117 as Id, '51.11.24' as Code, 'Деятельность агентов по оптовой торговле кормами для сельскохозяйственных животных' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1118 as Id, '51.11.25' as Code, 'Деятельность агентов по оптовой торговле текстильным сырьем и полуфабрикатами' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1119 as Id, '51.11.26' as Code, 'Деятельность агентов по оптовой торговле прочими сельскохозяйственным сырьем и полуфабрикатами, не включенными в другие группировки' as Name, 1111 ParentId, 0 as LastUpdateTick union all
select 1120 as Id, '51.12' as Code, 'Деятельность агентов по оптовой торговле топливом, рудами, металлами и химическими веществами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1121 as Id, '51.12.1' as Code, 'Деятельность агентов по оптовой торговле топливом' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1122 as Id, '51.12.2' as Code, 'Деятельность агентов по оптовой торговле рудами и металлами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1123 as Id, '51.12.21' as Code, 'Деятельность агентов по оптовой торговле рудами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1124 as Id, '51.12.22' as Code, 'Деятельность агентов по оптовой торговле черными металлами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1125 as Id, '51.12.23' as Code, 'Деятельность агентов по оптовой торговле цветными металлами, кроме драгоценных' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1126 as Id, '51.12.24' as Code, 'Деятельность агентов по оптовой торговле драгоценными металлами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1127 as Id, '51.12.3' as Code, 'Деятельность агентов по оптовой торговле химическими веществами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1128 as Id, '51.12.31' as Code, 'Деятельность агентов по оптовой торговле непищевым этиловым спиртом, включая денатурат' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1129 as Id, '51.12.32' as Code, 'Деятельность агентов по оптовой торговле удобрениями, пестицидами и прочими агрохимикатами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1130 as Id, '51.12.33' as Code, 'Деятельность агентов по оптовой торговле пластмассами и синтетическими смолами в первичных формах' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1131 as Id, '51.12.34' as Code, 'Деятельность агентов по оптовой торговле химическими волокнами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1132 as Id, '51.12.35' as Code, 'Деятельность агентов по оптовой торговле синтетическим каучуком и резиной в первичных формах' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1133 as Id, '51.12.36' as Code, 'Деятельность агентов по оптовой торговле взрывчатыми веществами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1134 as Id, '51.12.37' as Code, 'Деятельность агентов по оптовой торговле прочими основными химическими веществами' as Name, 1120 ParentId, 0 as LastUpdateTick union all
select 1135 as Id, '51.13' as Code, 'Деятельность агентов по оптовой торговле лесоматериалами и строительными материалами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1136 as Id, '51.13.1' as Code, 'Деятельность агентов по оптовой торговле лесоматериалами' as Name, 1135 ParentId, 0 as LastUpdateTick union all
select 1137 as Id, '51.13.2' as Code, 'Деятельность агентов по оптовой торговле строительными материалами' as Name, 1135 ParentId, 0 as LastUpdateTick union all
select 1138 as Id, '51.14' as Code, 'Деятельность агентов по оптовой торговле машинами, оборудованием, судами и летательными аппаратами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1139 as Id, '51.14.1' as Code, 'Деятельность агентов по оптовой торговле офисным оборудованием и вычислительной техникой' as Name, 1138 ParentId, 0 as LastUpdateTick union all
select 1140 as Id, '51.14.2' as Code, 'Деятельность агентов по оптовой торговле прочими видами машин и оборудования' as Name, 1138 ParentId, 0 as LastUpdateTick union all
select 1141 as Id, '51.14.3' as Code, 'Деятельность агентов по оптовой торговле судами и летательными  аппаратами' as Name, 1138 ParentId, 0 as LastUpdateTick union all
select 1142 as Id, '51.15' as Code, 'Деятельность агентов по оптовой торговле мебелью, бытовыми товарами,  скобяными, ножевыми и прочими металлическими изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1143 as Id, '51.15.1' as Code, 'Деятельность агентов по оптовой торговле бытовой мебелью' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1144 as Id, '51.15.2' as Code, 'Деятельность агентов по оптовой торговле скобяными, ножевыми и прочими бытовыми металлическими изделиями' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1145 as Id, '51.15.3' as Code, 'Деятельность агентов по оптовой торговле электротоварами и бытовыми  электроустановочными изделиями' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1146 as Id, '51.15.4' as Code, 'Деятельность агентов по оптовой торговле радио- и телеаппаратурой, техническими носителями информации (с записями и без записей)' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1147 as Id, '51.15.41' as Code, 'Деятельность агентов по оптовой торговле радио- и телеаппаратурой' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1148 as Id, '51.15.42' as Code, 'Деятельность агентов по оптовой торговле техническими носителями информации (с записями и без записей)' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1149 as Id, '51.15.5' as Code, 'Деятельность агентов по оптовой торговле прочими бытовыми товарами хозяйственного назначения' as Name, 1142 ParentId, 0 as LastUpdateTick union all
select 1150 as Id, '51.16' as Code, 'Деятельность агентов по оптовой торговле текстильными изделиями, одеждой, обувью, изделиями из кожи и меха' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1151 as Id, '51.16.1' as Code, 'Деятельность агентов по оптовой торговле текстильными изделиями' as Name, 1150 ParentId, 0 as LastUpdateTick union all
select 1152 as Id, '51.16.2' as Code, 'Деятельность агентов по оптовой торговле одеждой, включая одежду из кожи, аксессуарами одежды и обувью' as Name, 1150 ParentId, 0 as LastUpdateTick union all
select 1153 as Id, '51.16.3' as Code, 'Деятельность агентов по оптовой торговле изделиями из кожи и меха' as Name, 1150 ParentId, 0 as LastUpdateTick union all
select 1154 as Id, '51.17' as Code, 'Деятельность агентов по оптовой торговле пищевыми продуктами, включая  напитки, и табачными изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1155 as Id, '51.17.1' as Code, 'Деятельность агентов по оптовой торговле пищевыми продуктами' as Name, 1154 ParentId, 0 as LastUpdateTick union all
select 1156 as Id, '51.17.2' as Code, 'Деятельность агентов по оптовой торговле напитками' as Name, 1154 ParentId, 0 as LastUpdateTick union all
select 1157 as Id, '51.17.21' as Code, 'Деятельность агентов по оптовой торговле безалкогольными напитками' as Name, 1154 ParentId, 0 as LastUpdateTick union all
select 1158 as Id, '51.17.22' as Code, 'Деятельность агентов по оптовой торговле алкогольными напитками, кроме пива' as Name, 1154 ParentId, 0 as LastUpdateTick union all
select 1159 as Id, '51.17.23' as Code, 'Деятельность агентов по оптовой торговле пивом' as Name, 1154 ParentId, 0 as LastUpdateTick union all
select 1160 as Id, '51.17.3' as Code, 'Деятельность агентов по оптовой торговле табачными изделиями' as Name, 1154 ParentId, 0 as LastUpdateTick union all
select 1161 as Id, '51.18' as Code, 'Деятельность агентов, специализирующихся на оптовой торговле отдельными видами товаров или группами товаров, не включенными в другие группировки' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1162 as Id, '51.18.1' as Code, 'Деятельность агентов, специализирующихся на оптовой торговле фармацевтическими и медицинскими товарами, парфюмерными и косметическими товарами, включая мыло' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1163 as Id, '51.18.2' as Code, 'Деятельность агентов, специализирующихся на оптовой торговле товарами,  не включенными в другие группировки' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1164 as Id, '51.18.21' as Code, 'Деятельность агентов по оптовой торговле бумагой и бумажными  изделиями' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1165 as Id, '51.18.22' as Code, 'Деятельность агентов по оптовой торговле книгами' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1166 as Id, '51.18.23' as Code, 'Деятельность агентов по оптовой торговле газетами и журналами' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1167 as Id, '51.18.24' as Code, 'Деятельность агентов по оптовой торговле драгоценными камнями' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1168 as Id, '51.18.25' as Code, 'Деятельность агентов по оптовой торговле ювелирными изделиями' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1169 as Id, '51.18.26' as Code, 'Деятельность агентов по оптовой торговле электроэнергией и тепловой энергией (без их производства, передачи и распределения)' as Name, 1163 ParentId, 0 as LastUpdateTick union all
select 1170 as Id, '51.18.27' as Code, 'Деятельность агентов по оптовой торговле прочими товарами, не включенными в другие группировки' as Name, 1161 ParentId, 0 as LastUpdateTick union all
select 1171 as Id, '51.19' as Code, 'Деятельность агентов по оптовой торговле универсальным ассортиментом товаров' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1172 as Id, '51.2' as Code, 'Оптовая торговля сельскохозяйственным сырьем и живыми животными' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1173 as Id, '51.21' as Code, 'Оптовая торговля зерном, семенами и кормами для сельскохозяйственных  животных' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1174 as Id, '51.21.1' as Code, 'Оптовая торговля зерном' as Name, 1173 ParentId, 0 as LastUpdateTick union all
select 1175 as Id, '51.21.2' as Code, 'Оптовая торговля семенами, кроме масличных семян' as Name, 1173 ParentId, 0 as LastUpdateTick union all
select 1176 as Id, '51.21.3' as Code, 'Оптовая торговля масличными семенами и маслосодержащими плодами' as Name, 1173 ParentId, 0 as LastUpdateTick union all
select 1177 as Id, '51.21.4' as Code, 'Оптовая торговля кормами для сельскохозяйственных животных' as Name, 1173 ParentId, 0 as LastUpdateTick union all
select 1178 as Id, '51.21.5' as Code, 'Оптовая торговля сельскохозяйственным сырьем, не включенным в другие  группировки' as Name, 1173 ParentId, 0 as LastUpdateTick union all
select 1179 as Id, '51.22' as Code, 'Оптовая торговля цветами и другими растениями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1180 as Id, '51.23' as Code, 'Оптовая торговля живыми животными' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1181 as Id, '51.24' as Code, 'Оптовая торговля шкурами и кожей' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1182 as Id, '51.25' as Code, 'Оптовая торговля необработанным табаком' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1183 as Id, '51.3' as Code, 'Оптовая торговля пищевыми продуктами, включая напитки, и табачными изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1184 as Id, '51.31' as Code, 'Оптовая торговля фруктами, овощами и картофелем' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1185 as Id, '51.31.1' as Code, 'Оптовая торговля картофелем' as Name, 1184 ParentId, 0 as LastUpdateTick union all
select 1186 as Id, '51.31.2' as Code, 'Оптовая торговля непереработанными овощами, фруктами и орехами' as Name, 1184 ParentId, 0 as LastUpdateTick union all
select 1187 as Id, '51.32' as Code, 'Оптовая торговля мясом, мясом птицы, продуктами и консервами из мяса и мяса птицы' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1188 as Id, '51.32.1' as Code, 'Оптовая торговля мясом и мясом птицы, включая субпродукты' as Name, 1187 ParentId, 0 as LastUpdateTick union all
select 1189 as Id, '51.32.11' as Code, 'Оптовая торговля мясом, включая субпродукты' as Name, 1187 ParentId, 0 as LastUpdateTick union all
select 1190 as Id, '51.32.12' as Code, 'Оптовая торговля мясом птицы, включая субпродукты' as Name, 1187 ParentId, 0 as LastUpdateTick union all
select 1191 as Id, '51.32.2' as Code, 'Оптовая торговля продуктами из мяса и мяса птицы' as Name, 1187 ParentId, 0 as LastUpdateTick union all
select 1192 as Id, '51.32.3' as Code, 'Оптовая торговля консервами из мяса и мяса птицы' as Name, 1187 ParentId, 0 as LastUpdateTick union all
select 1193 as Id, '51.33' as Code, 'Оптовая торговля молочными продуктами, яйцами, пищевыми маслами и жирами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1194 as Id, '51.33.1' as Code, 'Оптовая торговля молочными продуктами' as Name, 1193 ParentId, 0 as LastUpdateTick union all
select 1195 as Id, '51.33.2' as Code, 'Оптовая торговля яйцами' as Name, 1193 ParentId, 0 as LastUpdateTick union all
select 1196 as Id, '51.33.3' as Code, 'Оптовая торговля пищевыми маслами и жирами' as Name, 1193 ParentId, 0 as LastUpdateTick union all
select 1197 as Id, '51.34' as Code, 'Оптовая торговля алкогольными и другими напитками' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1198 as Id, '51.34.1' as Code, 'Оптовая торговля безалкогольными напитками' as Name, 1197 ParentId, 0 as LastUpdateTick union all
select 1199 as Id, '51.34.2' as Code, 'Оптовая торговля алкогольными напитками, включая пиво' as Name, 1197 ParentId, 0 as LastUpdateTick union all
select 1200 as Id, '51.34.21' as Code, 'Оптовая торговля алкогольными напитками, кроме пива' as Name, 1197 ParentId, 0 as LastUpdateTick union all
select 1201 as Id, '51.34.22' as Code, 'Оптовая торговля пивом' as Name, 1197 ParentId, 0 as LastUpdateTick union all
select 1202 as Id, '51.35' as Code, 'Оптовая торговля табачными изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1203 as Id, '51.36' as Code, 'Оптовая торговля сахаром и сахаристыми кондитерскими изделиями, включая шоколад' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1204 as Id, '51.36.1' as Code, 'Оптовая торговля сахаром' as Name, 1203 ParentId, 0 as LastUpdateTick union all
select 1205 as Id, '51.36.2' as Code, 'Оптовая торговля сахаристыми кондитерскими изделиями, включая шоколад, мороженым и замороженными десертами' as Name, 1203 ParentId, 0 as LastUpdateTick union all
select 1206 as Id, '51.36.21' as Code, 'Оптовая торговля сахаристыми кондитерскими изделиями, включая шоколад' as Name, 1203 ParentId, 0 as LastUpdateTick union all
select 1207 as Id, '51.36.22' as Code, 'Оптовая торговля мороженым и замороженными десертами' as Name, 1203 ParentId, 0 as LastUpdateTick union all
select 1208 as Id, '51.37' as Code, 'Оптовая торговля кофе, чаем, какао и пряностями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1209 as Id, '51.38' as Code, 'Оптовая торговля прочими пищевыми продуктами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1210 as Id, '51.38.1' as Code, 'Оптовая торговля рыбой, морепродуктами и рыбными консервами' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1211 as Id, '51.38.2' as Code, 'Оптовая торговля прочими пищевыми продуктами' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1212 as Id, '51.38.21' as Code, 'Оптовая торговля переработанными овощами, картофелем, фруктами и орехами' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1213 as Id, '51.38.22' as Code, 'Оптовая торговля готовыми пищевыми продуктами, включая торговлю детским и диетическим питанием и прочими гомогенизированными пищевыми продуктами' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1214 as Id, '51.38.23' as Code, 'Оптовая торговля кормами для домашних животных' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1215 as Id, '51.38.24' as Code, 'Оптовая торговля хлебом и хлебобулочными изделиями' as Name, 1211 ParentId, 0 as LastUpdateTick union all
select 1216 as Id, '51.38.25' as Code, 'Оптовая торговля мучными кондитерскими изделиями' as Name, 1211 ParentId, 0 as LastUpdateTick union all
select 1217 as Id, '51.38.26' as Code, 'Оптовая торговля мукой и макаронными изделиями' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1218 as Id, '51.38.27' as Code, 'Оптовая торговля крупами' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1219 as Id, '51.38.28' as Code, 'Оптовая торговля солью' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1220 as Id, '51.38.29' as Code, 'Оптовая торговля прочими пищевыми продуктами, не включенными в другие группировки' as Name, 1209 ParentId, 0 as LastUpdateTick union all
select 1221 as Id, '51.39' as Code, 'Неспециализированная оптовая торговля пищевыми продуктами, включая  напитки, и табачными изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1222 as Id, '51.39.1' as Code, 'Неспециализированная оптовая торговля замороженными пищевыми  продуктами' as Name, 1221 ParentId, 0 as LastUpdateTick union all
select 1223 as Id, '51.39.2' as Code, 'Неспециализированная оптовая торговля незамороженными пищевыми  продуктами, напитками и табачными изделиями' as Name, 1221 ParentId, 0 as LastUpdateTick union all
select 1224 as Id, '51.4' as Code, 'Оптовая торговля непродовольственными потребительскими товарами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1225 as Id, '51.41' as Code, 'Оптовая торговля текстильными и галантерейными изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1226 as Id, '51.41.1' as Code, 'Оптовая торговля текстильными изделиями, кроме текстильных галантерейных изделий' as Name, 1225 ParentId, 0 as LastUpdateTick union all
select 1227 as Id, '51.41.2' as Code, 'Оптовая торговля галантерейными изделиями' as Name, 1225 ParentId, 0 as LastUpdateTick union all
select 1228 as Id, '51.42' as Code, 'Оптовая торговля одеждой, включая нательное белье, и обувью' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1229 as Id, '51.42.1' as Code, 'Оптовая торговля одеждой, кроме нательного белья' as Name, 1228 ParentId, 0 as LastUpdateTick union all
select 1230 as Id, '51.42.2' as Code, 'Оптовая торговля нательным бельем' as Name, 1228 ParentId, 0 as LastUpdateTick union all
select 1231 as Id, '51.42.3' as Code, 'Оптовая торговля изделиями из меха' as Name, 1228 ParentId, 0 as LastUpdateTick union all
select 1232 as Id, '51.42.4' as Code, 'Оптовая торговля обувью' as Name, 1228 ParentId, 0 as LastUpdateTick union all
select 1233 as Id, '51.42.5' as Code, 'Оптовая торговля аксессуарами одежды и головными уборами' as Name, 1228 ParentId, 0 as LastUpdateTick union all
select 1234 as Id, '51.43' as Code, 'Оптовая торговля бытовыми электротоварами, радио- и телеаппаратурой' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1235 as Id, '51.43.1' as Code, 'Оптовая торговля бытовыми электротоварами' as Name, 1234 ParentId, 0 as LastUpdateTick union all
select 1236 as Id, '51.43.2' as Code, 'Оптовая торговля радио- и телеаппаратурой, техническими носителями  информации (с записями и без записей)' as Name, 1234 ParentId, 0 as LastUpdateTick union all
select 1237 as Id, '51.43.21' as Code, 'Оптовая торговля радио- и телеаппаратурой' as Name, 1234 ParentId, 0 as LastUpdateTick union all
select 1238 as Id, '51.43.22' as Code, 'Оптовая торговля техническими носителями информации (с записями и без записей)' as Name, 1234 ParentId, 0 as LastUpdateTick union all
select 1239 as Id, '51.44' as Code, 'Оптовая торговля изделиями из керамики и стекла, обоями, чистящими средствами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1240 as Id, '51.44.1' as Code, 'Оптовая торговля ножевыми изделиями и бытовой металлической посудой' as Name, 1239 ParentId, 0 as LastUpdateTick union all
select 1241 as Id, '51.44.2' as Code, 'Оптовая торговля изделиями из керамики и стекла' as Name, 1239 ParentId, 0 as LastUpdateTick union all
select 1242 as Id, '51.44.3' as Code, 'Оптовая торговля обоями' as Name, 1239 ParentId, 0 as LastUpdateTick union all
select 1243 as Id, '51.44.4' as Code, 'Оптовая торговля чистящими средствами' as Name, 1239 ParentId, 0 as LastUpdateTick union all
select 1244 as Id, '51.45' as Code, 'Оптовая торговля парфюмерными и косметическими товарами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1245 as Id, '51.45.1' as Code, 'Оптовая торговля парфюмерными и косметическими товарами, кроме мыла' as Name, 1244 ParentId, 0 as LastUpdateTick union all
select 1246 as Id, '51.45.2' as Code, 'Оптовая торговля туалетным и хозяйственным мылом' as Name, 1244 ParentId, 0 as LastUpdateTick union all
select 1247 as Id, '51.46' as Code, 'Оптовая торговля фармацевтическими и медицинскими товарами, изделиями  медицинской техники и ортопедическими изделиями' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1248 as Id, '51.46.1' as Code, 'Оптовая торговля фармацевтическими и медицинскими товарами' as Name, 1247 ParentId, 0 as LastUpdateTick union all
select 1249 as Id, '51.46.2' as Code, 'Оптовая торговля изделиями медицинской техники и ортопедическими изделиями' as Name, 1247 ParentId, 0 as LastUpdateTick union all
select 1250 as Id, '51.47' as Code, 'Оптовая торговля прочими непродовольственными потребительскими товарами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1251 as Id, '51.47.1' as Code, 'Оптовая торговля бытовой мебелью, напольными покрытиями и прочими  неэлектрическими бытовыми товарами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1252 as Id, '51.47.11' as Code, 'Оптовая торговля бытовой мебелью' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1253 as Id, '51.47.12' as Code, 'Оптовая торговля неэлектрическими бытовыми приборами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1254 as Id, '51.47.13' as Code, 'Оптовая торговля плетеными изделиями, изделиями из пробки, бондарными изделиями и изделиями из дерева' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1255 as Id, '51.47.14' as Code, 'Оптовая торговля напольными покрытиями' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1256 as Id, '51.47.15' as Code, 'Оптовая торговля бытовыми товарами, не включенными в другие группировки' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1257 as Id, '51.47.2' as Code, 'Оптовая торговля книгами, газетами и журналами, писчебумажными и  канцелярскими товарами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1258 as Id, '51.47.21' as Code, 'Оптовая торговля книгами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1259 as Id, '51.47.22' as Code, 'Оптовая торговля газетами и журналами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1260 as Id, '51.47.23' as Code, 'Оптовая торговля писчебумажными и канцелярскими товарами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1261 as Id, '51.47.3' as Code, 'Оптовая торговля прочими потребительскими товарами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1262 as Id, '51.47.31' as Code, 'Оптовая торговля музыкальными инструментами и нотными изданиями' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1263 as Id, '51.47.32' as Code, 'Оптовая торговля фототоварами и оптическими товарами' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1264 as Id, '51.47.33' as Code, 'Оптовая торговля играми и игрушками' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1265 as Id, '51.47.34' as Code, 'Оптовая торговля ювелирными изделиями' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1266 as Id, '51.47.35' as Code, 'Оптовая торговля спортивными товарами, включая велосипеды' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1267 as Id, '51.47.36' as Code, 'Оптовая торговля изделиями из кожи и дорожными принадлежностями' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1268 as Id, '51.47.37' as Code, 'Оптовая торговля прочими потребительскими товарами, не включенными в другие группировки' as Name, 1250 ParentId, 0 as LastUpdateTick union all
select 1269 as Id, '51.5' as Code, 'Оптовая торговля несельскохозяйственными промежуточными продуктами, отходами и ломом' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1270 as Id, '51.51' as Code, 'Оптовая торговля топливом' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1271 as Id, '51.51.1' as Code, 'Оптовая торговля твердым топливом' as Name, 1270 ParentId, 0 as LastUpdateTick union all
select 1272 as Id, '51.51.2' as Code, 'Оптовая торговля моторным топливом, включая авиационный бензин' as Name, 1270 ParentId, 0 as LastUpdateTick union all
select 1273 as Id, '51.51.3' as Code, 'Оптовая торговля сырой нефтью' as Name, 1270 ParentId, 0 as LastUpdateTick union all
select 1274 as Id, '51.52' as Code, 'Оптовая торговля металлами и металлическими рудами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1275 as Id, '51.52.1' as Code, 'Оптовая торговля металлическими рудами' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1276 as Id, '51.52.11' as Code, 'Оптовая торговля железными рудами' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1277 as Id, '51.52.12' as Code, 'Оптовая торговля рудами цветных металлов' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1278 as Id, '51.52.2' as Code, 'Оптовая торговля металлами в первичных формах' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1279 as Id, '51.52.21' as Code, 'Оптовая торговля черными металлами в первичных формах' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1280 as Id, '51.52.22' as Code, 'Оптовая торговля цветными металлами в первичных формах, кроме драгоценных' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1281 as Id, '51.52.23' as Code, 'Оптовая торговля золотом и другими драгоценными металлами' as Name, 1274 ParentId, 0 as LastUpdateTick union all
select 1282 as Id, '51.53' as Code, 'Оптовая торговля лесоматериалами, строительными материалами и санитарно-техническим оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1283 as Id, '51.53.1' as Code, 'Оптовая торговля лесоматериалами' as Name, 1282 ParentId, 0 as LastUpdateTick union all
select 1284 as Id, '51.53.2' as Code, 'Оптовая торговля лакокрасочными материалами, листовым стеклом,  санитарно-техническим оборудованием и прочими строительными материалами' as Name, 1282 ParentId, 0 as LastUpdateTick union all
select 1285 as Id, '51.53.21' as Code, 'Оптовая торговля санитарно-техническим оборудованием' as Name, 1282 ParentId, 0 as LastUpdateTick union all
select 1286 as Id, '51.53.22' as Code, 'Оптовая торговля лакокрасочными материалами' as Name, 1282 ParentId, 0 as LastUpdateTick union all
select 1287 as Id, '51.53.23' as Code, 'Оптовая торговля материалами для остекления' as Name, 1282 ParentId, 0 as LastUpdateTick union all
select 1288 as Id, '51.53.24' as Code, 'Оптовая торговля прочими строительными материалами' as Name, 1282 ParentId, 0 as LastUpdateTick union all
select 1289 as Id, '51.54' as Code, 'Оптовая торговля скобяными изделиями, ручными инструментами,  водопроводным и отопительным оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1290 as Id, '51.54.1' as Code, 'Оптовая торговля скобяными изделиями' as Name, 1289 ParentId, 0 as LastUpdateTick union all
select 1291 as Id, '51.54.2' as Code, 'Оптовая торговля водопроводным и отопительным оборудованием' as Name, 1289 ParentId, 0 as LastUpdateTick union all
select 1292 as Id, '51.54.3' as Code, 'Оптовая торговля ручными инструментами' as Name, 1289 ParentId, 0 as LastUpdateTick union all
select 1293 as Id, '51.55' as Code, 'Оптовая торговля химическими продуктами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1294 as Id, '51.55.1' as Code, 'Оптовая торговля удобрениями, пестицидами и другими агрохимикатами' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1295 as Id, '51.55.11' as Code, 'Оптовая торговля удобрениями' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1296 as Id, '51.55.12' as Code, 'Оптовая торговля пестицидами и другими агрохимикатами' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1297 as Id, '51.55.2' as Code, 'Оптовая торговля синтетическими смолами и пластмассами в первичных формах' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1298 as Id, '51.55.3' as Code, 'Оптовая торговля прочими промышленными химическими веществами' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1299 as Id, '51.55.31' as Code, 'Оптовая торговля непищевым этиловым спиртом, включая денатурат' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1300 as Id, '51.55.32' as Code, 'Оптовая торговля синтетическим каучуком и резиной в первичных формах' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1301 as Id, '51.55.33' as Code, 'Оптовая торговля взрывчатыми веществами' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1302 as Id, '51.55.34' as Code, 'Оптовая торговля прочими промышленными химическими веществами, не включенными в другие группировки' as Name, 1293 ParentId, 0 as LastUpdateTick union all
select 1303 as Id, '51.56' as Code, 'Оптовая торговля прочими промежуточными продуктами' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1304 as Id, '51.56.1' as Code, 'Оптовая торговля бумагой и картоном' as Name, 1303 ParentId, 0 as LastUpdateTick union all
select 1305 as Id, '51.56.2' as Code, 'Оптовая торговля текстильными волокнами' as Name, 1303 ParentId, 0 as LastUpdateTick union all
select 1306 as Id, '51.56.3' as Code, 'Оптовая торговля драгоценными камнями' as Name, 1303 ParentId, 0 as LastUpdateTick union all
select 1307 as Id, '51.56.4' as Code, 'Оптовая торговля электрической и тепловой энергией (без их передачи и распределения)' as Name, 1303 ParentId, 0 as LastUpdateTick union all
select 1308 as Id, '51.56.5' as Code, 'Оптовая торговля прочими промежуточными продуктами, кроме сельскохозяйственных, не включенными в другие группировки' as Name, 1303 ParentId, 0 as LastUpdateTick union all
select 1309 as Id, '51.57' as Code, 'Оптовая торговля отходами и ломом' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1310 as Id, '51.6' as Code, 'Оптовая торговля машинами и оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1311 as Id, '51.61' as Code, 'Оптовая торговля станками' as Name, 1310 ParentId, 0 as LastUpdateTick union all
select 1312 as Id, '51.61.1' as Code, 'Оптовая торговля деревообрабатывающими станками' as Name, 1311 ParentId, 0 as LastUpdateTick union all
select 1313 as Id, '51.61.2' as Code, 'Оптовая торговля станками для обработки металлов' as Name, 1311 ParentId, 0 as LastUpdateTick union all
select 1314 as Id, '51.61.3' as Code, 'Оптовая торговля станками для обработки прочих материалов' as Name, 1311 ParentId, 0 as LastUpdateTick union all
select 1315 as Id, '51.62' as Code, 'Оптовая торговля машинами и оборудованием для строительства' as Name, 1310 ParentId, 0 as LastUpdateTick union all
select 1316 as Id, '51.63' as Code, 'Оптовая торговля машинами и оборудованием для текстильного, швейного и трикотажного производств' as Name, 1310 ParentId, 0 as LastUpdateTick union all
select 1317 as Id, '51.64' as Code, 'Оптовая торговля офисными машинами и оборудованием' as Name, 1310 ParentId, 0 as LastUpdateTick union all
select 1318 as Id, '51.64.1' as Code, 'Оптовая торговля офисными машинами' as Name, 1317 ParentId, 0 as LastUpdateTick union all
select 1319 as Id, '51.64.2' as Code, 'Оптовая торговля компьютерами и периферийными устройствами' as Name, 1317 ParentId, 0 as LastUpdateTick union all
select 1320 as Id, '51.64.3' as Code, 'Оптовая торговля офисной мебелью' as Name, 1317 ParentId, 0 as LastUpdateTick union all
select 1321 as Id, '51.65' as Code, 'Оптовая торговля прочими машинами и оборудованием' as Name, 1310 ParentId, 0 as LastUpdateTick union all
select 1322 as Id, '51.65.1' as Code, 'Оптовая торговля транспортными средствами и оборудованием' as Name, 1321 ParentId, 0 as LastUpdateTick union all
select 1323 as Id, '51.65.2' as Code, 'Оптовая торговля эксплуатационными материалами и принадлежностями машин и оборудования' as Name, 1321 ParentId, 0 as LastUpdateTick union all
select 1324 as Id, '51.65.3' as Code, 'Оптовая торговля подъемно-транспортными машинами и оборудованием' as Name, 1321 ParentId, 0 as LastUpdateTick union all
select 1325 as Id, '51.65.4' as Code, 'Оптовая торговля машинами и оборудованием для производства пищевых продуктов, включая напитки, и табачных изделий' as Name, 1321 ParentId, 0 as LastUpdateTick union all
select 1326 as Id, '51.65.5' as Code, 'Оптовая торговля производственным электрическим и электронным оборудованием, включая оборудование электросвязи' as Name, 1321 ParentId, 0 as LastUpdateTick union all
select 1327 as Id, '51.65.6' as Code, 'Оптовая торговля прочими машинами, приборами, оборудованием общепромышленного и специального назначения' as Name, 1321 ParentId, 0 as LastUpdateTick union all
select 1328 as Id, '51.66' as Code, 'Оптовая торговля машинами и оборудованием для сельского хозяйства' as Name, 1310 ParentId, 0 as LastUpdateTick union all
select 1329 as Id, '51.66.1' as Code, 'Оптовая торговля тракторами' as Name, 1328 ParentId, 0 as LastUpdateTick union all
select 1330 as Id, '51.66.2' as Code, 'Оптовая торговля прочими машинами и оборудованием для сельского и лесного хозяйства' as Name, 1328 ParentId, 0 as LastUpdateTick union all
select 1331 as Id, '51.7' as Code, 'Прочая оптовая торговля' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 1332 as Id, '51.70' as Code, 'Прочая оптовая торговля' as Name, 1331 ParentId, 0 as LastUpdateTick union all
select 1333 as Id, '52' as Code, 'Розничная торговля, кроме торговли автотранспортными средствами и мотоциклами; ремонт бытовых изделий и предметов личного пользования' as Name, 2426 ParentId, 0 as LastUpdateTick union all
select 1334 as Id, '52.1' as Code, 'Розничная торговля в неспециализированных магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1335 as Id, '52.11' as Code, 'Розничная торговля в неспециализированных магазинах преимущественно пищевыми продуктами, включая напитки, и табачными изделиями' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1336 as Id, '52.11.1' as Code, 'Розничная торговля в неспециализированных магазинах замороженными  продуктами' as Name, 1335 ParentId, 0 as LastUpdateTick union all
select 1337 as Id, '52.11.2' as Code, 'Розничная торговля в неспециализированных магазинах незамороженными  продуктами, включая напитки, и табачными изделиями' as Name, 1335 ParentId, 0 as LastUpdateTick union all
select 1338 as Id, '52.12' as Code, 'Прочая розничная торговля в неспециализированных магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1339 as Id, '52.2' as Code, 'Розничная торговля пищевыми продуктами, включая напитки, и табачными  изделиями в специализированных магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1340 as Id, '52.21' as Code, 'Розничная торговля фруктами, овощами и картофелем' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1341 as Id, '52.22' as Code, 'Розничная торговля мясом, мясом птицы, продуктами и консервами из мяса и мяса птицы' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1342 as Id, '52.22.1' as Code, 'Розничная торговля мясом и мясом птицы, включая субпродукты' as Name, 1341 ParentId, 0 as LastUpdateTick union all
select 1343 as Id, '52.22.2' as Code, 'Розничная торговля продуктами из мяса и мяса птицы' as Name, 1341 ParentId, 0 as LastUpdateTick union all
select 1344 as Id, '52.22.3' as Code, 'Розничная торговля консервами из мяса и мяса птицы' as Name, 1341 ParentId, 0 as LastUpdateTick union all
select 1345 as Id, '52.23' as Code, 'Розничная торговля рыбой, ракообразными и моллюсками' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1346 as Id, '52.23.1' as Code, 'Розничная торговля рыбой и морепродуктами' as Name, 1345 ParentId, 0 as LastUpdateTick union all
select 1347 as Id, '52.23.2' as Code, 'Розничная торговля консервами из рыбы и морепродуктов' as Name, 1345 ParentId, 0 as LastUpdateTick union all
select 1348 as Id, '52.24' as Code, 'Розничная торговля хлебом, хлебобулочными и кондитерскими изделиями' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1349 as Id, '52.24.1' as Code, 'Розничная торговля хлебом и хлебобулочными изделиями' as Name, 1348 ParentId, 0 as LastUpdateTick union all
select 1350 as Id, '52.24.2' as Code, 'Розничная торговля кондитерскими изделиями' as Name, 1348 ParentId, 0 as LastUpdateTick union all
select 1351 as Id, '52.24.21' as Code, 'Розничная торговля мучными кондитерскими изделиями' as Name, 1348 ParentId, 0 as LastUpdateTick union all
select 1352 as Id, '52.24.22' as Code, 'Розничная торговля сахаристыми кондитерскими изделиями, включая  шоколад' as Name, 1348 ParentId, 0 as LastUpdateTick union all
select 1353 as Id, '52.24.3' as Code, 'Розничная торговля мороженым и замороженными десертами' as Name, 1348 ParentId, 0 as LastUpdateTick union all
select 1354 as Id, '52.25' as Code, 'Розничная торговля алкогольными и другими напитками' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1355 as Id, '52.25.1' as Code, 'Розничная торговля алкогольными напитками, включая пиво' as Name, 1354 ParentId, 0 as LastUpdateTick union all
select 1356 as Id, '52.25.11' as Code, 'Розничная торговля алкогольными напитками, кроме пива' as Name, 1354 ParentId, 0 as LastUpdateTick union all
select 1357 as Id, '52.25.12' as Code, 'Розничная торговля пивом' as Name, 1354 ParentId, 0 as LastUpdateTick union all
select 1358 as Id, '52.25.2' as Code, 'Розничная торговля безалкогольными напитками' as Name, 1354 ParentId, 0 as LastUpdateTick union all
select 1359 as Id, '52.26' as Code, 'Розничная торговля табачными изделиями' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1360 as Id, '52.27' as Code, 'Прочая розничная торговля пищевыми продуктами в специализированных  магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1361 as Id, '52.27.1' as Code, 'Розничная торговля молочными продуктами и яйцами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1362 as Id, '52.27.11' as Code, 'Розничная торговля молочными продуктами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1363 as Id, '52.27.12' as Code, 'Розничная торговля яйцами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1364 as Id, '52.27.2' as Code, 'Розничная торговля пищевыми маслами и жирами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1365 as Id, '52.27.21' as Code, 'Розничная торговля животными маслами и жирами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1366 as Id, '52.27.22' as Code, 'Розничная торговля растительными маслами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1367 as Id, '52.27.3' as Code, 'Розничная торговля прочими пищевыми продуктами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1368 as Id, '52.27.31' as Code, 'Розничная торговля мукой и макаронными изделиями' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1369 as Id, '52.27.32' as Code, 'Розничная торговля крупами' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1370 as Id, '52.27.33' as Code, 'Розничная торговля консервированными фруктами, овощами, орехами и  т.п.' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1371 as Id, '52.27.34' as Code, 'Розничная торговля сахаром' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1372 as Id, '52.27.35' as Code, 'Розничная торговля солью' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1373 as Id, '52.27.36' as Code, 'Розничная торговля чаем, кофе, какао' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1374 as Id, '52.27.39' as Code, 'Розничная торговля прочими пищевыми продуктами, не включенными в  другие группировки' as Name, 1360 ParentId, 0 as LastUpdateTick union all
select 1375 as Id, '52.3' as Code, 'Розничная торговля фармацевтическими и медицинскими товарами,  косметическими и парфюмерными товарами' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1376 as Id, '52.31' as Code, 'Розничная торговля фармацевтическими товарами' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1377 as Id, '52.32' as Code, 'Розничная торговля медицинскими товарами и ортопедическими изделиями' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1378 as Id, '52.33' as Code, 'Розничная торговля косметическими и парфюмерными товарами' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1379 as Id, '52.33.1' as Code, 'Розничная торговля косметическими и парфюмерными товарами, кроме мыла' as Name, 1378 ParentId, 0 as LastUpdateTick union all
select 1380 as Id, '52.33.2' as Code, 'Розничная торговля туалетным и хозяйственным мылом' as Name, 1378 ParentId, 0 as LastUpdateTick union all
select 1381 as Id, '52.4' as Code, 'Прочая розничная торговля в специализированных магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1382 as Id, '52.41' as Code, 'Розничная торговля текстильными и галантерейными изделиями' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1383 as Id, '52.41.1' as Code, 'Розничная торговля текстильными изделиями' as Name, 1382 ParentId, 0 as LastUpdateTick union all
select 1384 as Id, '52.41.2' as Code, 'Розничная торговля галантерейными изделиями' as Name, 1382 ParentId, 0 as LastUpdateTick union all
select 1385 as Id, '52.42' as Code, 'Розничная торговля одеждой' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1386 as Id, '52.42.1' as Code, 'Розничная торговля мужской, женской и детской одеждой' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1387 as Id, '52.42.2' as Code, 'Розничная торговля нательным бельем' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1388 as Id, '52.42.3' as Code, 'Розничная торговля изделиями из меха' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1389 as Id, '52.42.4' as Code, 'Розничная торговля одеждой из кожи' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1390 as Id, '52.42.5' as Code, 'Розничная торговля спортивной одеждой' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1391 as Id, '52.42.6' as Code, 'Розничная торговля чулочно-носочными изделиями' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1392 as Id, '52.42.7' as Code, 'Розничная торговля головными уборами' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1393 as Id, '52.42.8' as Code, 'Розничная торговля аксессуарами одежды (перчатками, галстуками, шарфами, ремнями, подтяжками и т.п.)' as Name, 1385 ParentId, 0 as LastUpdateTick union all
select 1394 as Id, '52.43' as Code, 'Розничная торговля обувью и изделиями из кожи' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1395 as Id, '52.43.1' as Code, 'Розничная торговля обувью' as Name, 1394 ParentId, 0 as LastUpdateTick union all
select 1396 as Id, '52.43.2' as Code, 'Розничная торговля изделиями из кожи и дорожными принадлежностями' as Name, 1394 ParentId, 0 as LastUpdateTick union all
select 1397 as Id, '52.44' as Code, 'Розничная торговля мебелью и товарами для дома' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1398 as Id, '52.44.1' as Code, 'Розничная торговля мебелью' as Name, 1397 ParentId, 0 as LastUpdateTick union all
select 1399 as Id, '52.44.2' as Code, 'Розничная торговля различной домашней утварью, ножевыми изделиями, посудой, изделиями из стекла и керамики, в том числе фарфора и фаянса' as Name, 1397 ParentId, 0 as LastUpdateTick union all
select 1400 as Id, '52.44.3' as Code, 'Розничная торговля светильниками' as Name, 1397 ParentId, 0 as LastUpdateTick union all
select 1401 as Id, '52.44.4' as Code, 'Розничная торговля портьерами, тюлевыми занавесями и другими  предметами домашнего обихода из текстильных материалов' as Name, 1397 ParentId, 0 as LastUpdateTick union all
select 1402 as Id, '52.44.5' as Code, 'Розничная торговля изделиями из дерева, пробки и плетеными изделиями' as Name, 1397 ParentId, 0 as LastUpdateTick union all
select 1403 as Id, '52.44.6' as Code, 'Розничная торговля бытовыми изделиями и приборами, не включенными в  другие группировки' as Name, 1397 ParentId, 0 as LastUpdateTick union all
select 1404 as Id, '52.45' as Code, 'Розничная торговля бытовыми электротоварами, радио- и телеаппаратурой' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1405 as Id, '52.45.1' as Code, 'Розничная торговля бытовыми электротоварами' as Name, 1404 ParentId, 0 as LastUpdateTick union all
select 1406 as Id, '52.45.2' as Code, 'Розничная торговля радио- и телеаппаратурой' as Name, 1404 ParentId, 0 as LastUpdateTick union all
select 1407 as Id, '52.45.3' as Code, 'Розничная торговля аудио- и видеоаппаратурой' as Name, 1404 ParentId, 0 as LastUpdateTick union all
select 1408 as Id, '52.45.4' as Code, 'Розничная торговля техническими носителями информации (с записями и без записей)' as Name, 1404 ParentId, 0 as LastUpdateTick union all
select 1409 as Id, '52.45.5' as Code, 'Розничная торговля музыкальными инструментами и нотными изданиями' as Name, 1404 ParentId, 0 as LastUpdateTick union all
select 1410 as Id, '52.46' as Code, 'Розничная торговля скобяными изделиями, лакокрасочными материалами и  материалами для остекления' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1411 as Id, '52.46.1' as Code, 'Розничная торговля скобяными изделиями' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1412 as Id, '52.46.2' as Code, 'Розничная торговля красками, лаками и эмалями' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1413 as Id, '52.46.3' as Code, 'Розничная торговля материалами для остекления' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1414 as Id, '52.46.4' as Code, 'Розничная торговля материалами и оборудованием для изготовления поделок' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1415 as Id, '52.46.5' as Code, 'Розничная торговля санитарно-техническим оборудованием' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1416 as Id, '52.46.6' as Code, 'Розничная торговля садово-огородной техникой и инвентарем' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1417 as Id, '52.46.7' as Code, 'Розничная торговля строительными материалами, не включенными в другие  группировки' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1418 as Id, '52.46.71' as Code, 'Розничная торговля лесоматериалами' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1419 as Id, '52.46.72' as Code, 'Розничная торговля кирпичом' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1420 as Id, '52.46.73' as Code, 'Розничная торговля металлическими и неметаллическими конструкциями и  т.п.' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 1421 as Id, '52.47' as Code, 'Розничная торговля книгами, журналами, газетами, писчебумажными и  канцелярскими товарами' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1422 as Id, '52.47.1' as Code, 'Розничная торговля книгами' as Name, 1421 ParentId, 0 as LastUpdateTick union all
select 1423 as Id, '52.47.2' as Code, 'Розничная торговля газетами и журналами' as Name, 1421 ParentId, 0 as LastUpdateTick union all
select 1424 as Id, '52.47.3' as Code, 'Розничная торговля писчебумажными и канцелярскими товарами' as Name, 1421 ParentId, 0 as LastUpdateTick union all
select 1425 as Id, '52.48' as Code, 'Прочая розничная торговля в специализированных магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1426 as Id, '52.48.1' as Code, 'Специализированная розничная торговля офисной мебелью, офисным  оборудованием, компьютерами, оптическими приборами и фотоаппаратурой' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1427 as Id, '52.48.11' as Code, 'Розничная торговля офисной мебелью' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1428 as Id, '52.48.12' as Code, 'Розничная торговля офисными машинами и оборудованием' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1429 as Id, '52.48.13' as Code, 'Розничная торговля компьютерами, программным обеспечением и периферийными устройствами' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1430 as Id, '52.48.14' as Code, 'Розничная торговля фотоаппаратурой, оптическими приборами и средствами измерений, кроме очков' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1431 as Id, '52.48.15' as Code, 'Розничная торговля очками, включая сборку и ремонт очков' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1432 as Id, '52.48.2' as Code, 'Специализированная розничная торговля часами, ювелирными изделиями,  спортивными товарами, играми и игрушками' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1433 as Id, '52.48.21' as Code, 'Розничная торговля часами' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1434 as Id, '52.48.22' as Code, 'Розничная торговля ювелирными изделиями' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1435 as Id, '52.48.23' as Code, 'Розничная торговля спортивными товарами, рыболовными принадлежностями, туристским снаряжением, лодками и велосипедами' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1436 as Id, '52.48.24' as Code, 'Розничная торговля играми и игрушками' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1437 as Id, '52.48.3' as Code, 'Специализированная розничная торговля непродовольственными товарами, не включенными в другие группировки' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1438 as Id, '52.48.31' as Code, 'Розничная торговля товарами бытовой химии, синтетическими моющими средствами, обоями и напольными покрытиями' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1439 as Id, '52.48.32' as Code, 'Розничная торговля цветами и другими растениями, семенами и удобрениями' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1440 as Id, '52.48.33' as Code, 'Розничная торговля домашними животными и кормом для домашних животных' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1441 as Id, '52.48.34' as Code, 'Розничная торговля сувенирами, изделиями народных художественных промыслов, предметами культового и религиозного назначения, похоронными принадлежностями' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1442 as Id, '52.48.35' as Code, 'Розничная торговля бытовым жидким котельным топливом, газом в  баллонах, углем, древесным топливом, топливным торфом' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1443 as Id, '52.48.36' as Code, 'Розничная торговля филателистическими и нумизматическими товарами' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1444 as Id, '52.48.37' as Code, 'Розничная торговля произведениями искусства в коммерческих художественных галереях' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1445 as Id, '52.48.38' as Code, 'Розничная торговля пиротехническими средствами' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1446 as Id, '52.48.39' as Code, 'Специализированная розничная торговля прочими непродовольственными  товарами, не включенными в другие группировки' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 1447 as Id, '52.5' as Code, 'Розничная торговля бывшими в употреблении товарами в магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1448 as Id, '52.50' as Code, 'Розничная торговля бывшими в употреблении товарами в магазинах' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1449 as Id, '52.50.1' as Code, 'Розничная торговля предметами антиквариата' as Name, 1448 ParentId, 0 as LastUpdateTick union all
select 1450 as Id, '52.50.2' as Code, 'Розничная торговля букинистическими книгами' as Name, 1448 ParentId, 0 as LastUpdateTick union all
select 1451 as Id, '52.50.3' as Code, 'Розничная торговля прочими бывшими в употреблении товарами' as Name, 1448 ParentId, 0 as LastUpdateTick union all
select 1452 as Id, '52.6' as Code, 'Розничная торговля вне магазинов' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1453 as Id, '52.61' as Code, 'Розничная торговля по заказам' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1454 as Id, '52.61.1' as Code, 'Розничная почтовая (посылочная) торговля' as Name, 1453 ParentId, 0 as LastUpdateTick union all
select 1455 as Id, '52.61.2' as Code, 'Розничная торговля, осуществляемая непосредственно при помощи телевидения, радио, телефона и Интернет' as Name, 1453 ParentId, 0 as LastUpdateTick union all
select 1456 as Id, '52.62' as Code, 'Розничная торговля в палатках и на рынках' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1457 as Id, '52.63' as Code, 'Прочая розничная торговля вне магазинов' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1458 as Id, '52.7' as Code, 'Ремонт бытовых изделий и предметов личного пользования' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1459 as Id, '52.71' as Code, 'Ремонт обуви и прочих изделий из кожи' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1460 as Id, '52.72' as Code, 'Ремонт бытовых электрических изделий' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1461 as Id, '52.72.1' as Code, 'Ремонт радио- и телеаппаратуры и прочей аудио- и видеоаппаратуры' as Name, 1460 ParentId, 0 as LastUpdateTick union all
select 1462 as Id, '52.72.2' as Code, 'Ремонт прочих бытовых электрических изделий' as Name, 1460 ParentId, 0 as LastUpdateTick union all
select 1463 as Id, '52.73' as Code, 'Ремонт часов и ювелирных изделий' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1464 as Id, '52.74' as Code, 'Ремонт бытовых изделий и предметов личного пользования, не включенных в другие группировки' as Name, 1333 ParentId, 0 as LastUpdateTick union all
select 1465 as Id, '55' as Code, 'Деятельность гостиниц и ресторанов' as Name, 2427 ParentId, 0 as LastUpdateTick union all
select 1466 as Id, '55.1' as Code, 'Деятельность гостиниц' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1467 as Id, '55.11' as Code, 'Деятельность гостиниц с ресторанами' as Name, 1466 ParentId, 0 as LastUpdateTick union all
select 1468 as Id, '55.12' as Code, 'Деятельность гостиниц без ресторанов' as Name, 1466 ParentId, 0 as LastUpdateTick union all
select 1469 as Id, '55.2' as Code, 'Деятельность прочих мест для временного проживания' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1470 as Id, '55.21' as Code, 'Деятельность молодежных туристских лагерей и горных туристских баз' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1471 as Id, '55.22' as Code, 'Деятельность кемпингов' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1472 as Id, '55.23' as Code, 'Деятельность прочих мест для проживания' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1473 as Id, '55.23.1' as Code, 'Деятельность детских лагерей на время каникул' as Name, 1472 ParentId, 0 as LastUpdateTick union all
select 1474 as Id, '55.23.2' as Code, 'Деятельность пансионатов, домов отдыха и т.п.' as Name, 1472 ParentId, 0 as LastUpdateTick union all
select 1475 as Id, '55.23.3' as Code, 'Сдача внаем для временного проживания меблированных комнат' as Name, 1472 ParentId, 0 as LastUpdateTick union all
select 1476 as Id, '55.23.4' as Code, 'Предоставление мест для временного проживания в железнодорожных спальных вагонах и прочих транспортных средствах' as Name, 1472 ParentId, 0 as LastUpdateTick union all
select 1477 as Id, '55.23.5' as Code, 'Деятельность прочих мест для временного проживания, не включенных в другие группировки' as Name, 1472 ParentId, 0 as LastUpdateTick union all
select 1478 as Id, '55.3' as Code, 'Деятельность ресторанов' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1479 as Id, '55.30' as Code, 'Деятельность ресторанов и кафе' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1480 as Id, '55.4' as Code, 'Деятельность баров' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1481 as Id, '55.40' as Code, 'Деятельность баров' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1482 as Id, '55.5' as Code, 'Деятельность столовых при предприятиях и учреждениях и поставка продукции  общественного питания' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1483 as Id, '55.51' as Code, 'Деятельность столовых при предприятиях и учреждениях' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1484 as Id, '55.52' as Code, 'Поставка продукции общественного питания' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 1485 as Id, '60' as Code, 'Деятельность сухопутного транспорта' as Name, 2428 ParentId, 0 as LastUpdateTick union all
select 1486 as Id, '60.1' as Code, 'Деятельность железнодорожного транспорта' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1487 as Id, '60.10' as Code, 'Деятельность железнодорожного транспорта' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1488 as Id, '60.10.1' as Code, 'Деятельность магистрального железнодорожного транспорта' as Name, 1487 ParentId, 0 as LastUpdateTick union all
select 1489 as Id, '60.10.11' as Code, 'Деятельность магистрального пассажирского железнодорожного транспорта' as Name, 1487 ParentId, 0 as LastUpdateTick union all
select 1490 as Id, '60.10.12' as Code, 'Деятельность магистрального грузового железнодорожного транспорта' as Name, 1487 ParentId, 0 as LastUpdateTick union all
select 1491 as Id, '60.10.2' as Code, 'Деятельность промышленного железнодорожного транспорта' as Name, 1487 ParentId, 0 as LastUpdateTick union all
select 1492 as Id, '60.2' as Code, 'Деятельность прочего сухопутного транспорта' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1493 as Id, '60.21' as Code, 'Деятельность прочего сухопутного пассажирского транспорта, подчиняющегося расписанию' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1494 as Id, '60.21.1' as Code, 'Деятельность автомобильного (автобусного) пассажирского транспорта,  подчиняющегося расписанию' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1495 as Id, '60.21.11' as Code, 'Внутригородские автомобильные (автобусные) пассажирские перевозки,  подчиняющиеся расписанию' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1496 as Id, '60.21.12' as Code, 'Пригородные автомобильные (автобусные) пассажирские перевозки,  подчиняющиеся расписанию' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1497 as Id, '60.21.13' as Code, 'Междугородные автомобильные (автобусные) пассажирские перевозки,  подчиняющиеся расписанию' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1498 as Id, '60.21.14' as Code, 'Международные автомобильные (автобусные) пассажирские перевозки, подчиняющиеся расписанию' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1499 as Id, '60.21.2' as Code, 'Деятельность городского электрического транспорта' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1500 as Id, '60.21.21' as Code, 'Деятельность трамвайного транспорта' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1501 as Id, '60.21.22' as Code, 'Деятельность троллейбусного транспорта' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1502 as Id, '60.21.23' as Code, 'Деятельность метрополитена' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1503 as Id, '60.21.3' as Code, 'Пассажирские перевозки фуникулерами, воздушными канатными дорогами  и подъемниками' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 1504 as Id, '60.22' as Code, 'Деятельность такси' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1505 as Id, '60.23' as Code, 'Деятельность прочего сухопутного пассажирского транспорта' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1506 as Id, '60.24' as Code, 'Деятельность автомобильного грузового транспорта' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1507 as Id, '60.24.1' as Code, 'Деятельность автомобильного грузового специализированного транспорта' as Name, 1506 ParentId, 0 as LastUpdateTick union all
select 1508 as Id, '60.24.2' as Code, 'Деятельность автомобильного грузового неспециализированного транспорта' as Name, 1506 ParentId, 0 as LastUpdateTick union all
select 1509 as Id, '60.24.3' as Code, 'Аренда грузового автомобильного транспорта с водителем' as Name, 1506 ParentId, 0 as LastUpdateTick union all
select 1510 as Id, '60.3' as Code, 'Транспортирование по трубопроводам' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1511 as Id, '60.30' as Code, 'Транспортирование по трубопроводам' as Name, 1485 ParentId, 0 as LastUpdateTick union all
select 1512 as Id, '60.30.1' as Code, 'Транспортирование по трубопроводам нефти и нефтепродуктов' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1513 as Id, '60.30.11' as Code, 'Транспортирование по трубопроводам нефти' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1514 as Id, '60.30.12' as Code, 'Транспортирование по трубопроводам нефтепродуктов' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1515 as Id, '60.30.2' as Code, 'Транспортирование по трубопроводам газа и продуктов его переработки' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1516 as Id, '60.30.21' as Code, 'Транспортирование по трубопроводам газа' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1517 as Id, '60.30.22' as Code, 'Транспортирование по трубопроводам продуктов переработки газа' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1518 as Id, '60.30.3' as Code, 'Транспортирование по трубопроводам прочих видов грузов' as Name, 1511 ParentId, 0 as LastUpdateTick union all
select 1519 as Id, '61' as Code, 'Деятельность водного транспорта' as Name, 2428 ParentId, 0 as LastUpdateTick union all
select 1520 as Id, '61.1' as Code, 'Деятельность морского транспорта' as Name, 1519 ParentId, 0 as LastUpdateTick union all
select 1521 as Id, '61.10' as Code, 'Деятельность морского транспорта' as Name, 1519 ParentId, 0 as LastUpdateTick union all
select 1522 as Id, '61.10.1' as Code, 'Деятельность морского пассажирского транспорта' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 1523 as Id, '61.10.2' as Code, 'Деятельность морского грузового транспорта' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 1524 as Id, '61.10.3' as Code, 'Аренда морских транспортных средств с экипажем; предоставление маневровых услуг' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 1525 as Id, '61.2' as Code, 'Деятельность внутреннего водного транспорта' as Name, 1519 ParentId, 0 as LastUpdateTick union all
select 1526 as Id, '61.20' as Code, 'Деятельность внутреннего водного транспорта' as Name, 1519 ParentId, 0 as LastUpdateTick union all
select 1527 as Id, '61.20.1' as Code, 'Деятельность внутреннего водного пассажирского транспорта' as Name, 1526 ParentId, 0 as LastUpdateTick union all
select 1528 as Id, '61.20.2' as Code, 'Деятельность внутреннего водного грузового транспорта' as Name, 1526 ParentId, 0 as LastUpdateTick union all
select 1529 as Id, '61.20.3' as Code, 'Аренда внутренних водных транспортных средств с экипажем; предоставление маневровых услуг' as Name, 1526 ParentId, 0 as LastUpdateTick union all
select 1530 as Id, '61.20.4' as Code, 'Деятельность по обеспечению лесосплава (без сплава в плотах судовой  тягой)' as Name, 1526 ParentId, 0 as LastUpdateTick union all
select 1531 as Id, '62' as Code, 'Деятельность воздушного и космического транспорта' as Name, 2428 ParentId, 0 as LastUpdateTick union all
select 1532 as Id, '62.1' as Code, 'Деятельность воздушного транспорта, подчиняющегося расписанию' as Name, 1531 ParentId, 0 as LastUpdateTick union all
select 1533 as Id, '62.10' as Code, 'Деятельность воздушного транспорта, подчиняющегося расписанию' as Name, 1531 ParentId, 0 as LastUpdateTick union all
select 1534 as Id, '62.10.1' as Code, 'Деятельность воздушного пассажирского транспорта, подчиняющегося  расписанию' as Name, 1533 ParentId, 0 as LastUpdateTick union all
select 1535 as Id, '62.10.2' as Code, 'Деятельность воздушного грузового транспорта, подчиняющегося расписанию' as Name, 1533 ParentId, 0 as LastUpdateTick union all
select 1536 as Id, '62.2' as Code, 'Деятельность воздушного транспорта, не подчиняющегося расписанию' as Name, 1531 ParentId, 0 as LastUpdateTick union all
select 1537 as Id, '62.20' as Code, 'Деятельность воздушного транспорта, не подчиняющегося расписанию' as Name, 1531 ParentId, 0 as LastUpdateTick union all
select 1538 as Id, '62.20.1' as Code, 'Деятельность воздушного пассажирского транспорта, не подчиняющегося  расписанию' as Name, 1537 ParentId, 0 as LastUpdateTick union all
select 1539 as Id, '62.20.2' as Code, 'Деятельность воздушного грузового транспорта, не подчиняющегося  расписанию' as Name, 1537 ParentId, 0 as LastUpdateTick union all
select 1540 as Id, '62.20.3' as Code, 'Аренда воздушного транспорта с экипажем' as Name, 1537 ParentId, 0 as LastUpdateTick union all
select 1541 as Id, '62.3' as Code, 'Деятельность космического транспорта' as Name, 1531 ParentId, 0 as LastUpdateTick union all
select 1542 as Id, '62.30' as Code, 'Деятельность космического транспорта' as Name, 1531 ParentId, 0 as LastUpdateTick union all
select 1543 as Id, '62.30.1' as Code, 'Выведение космических объектов в космическое пространство' as Name, 1542 ParentId, 0 as LastUpdateTick union all
select 1544 as Id, '62.30.11' as Code, 'Подготовка ракет космического назначения к пуску' as Name, 1542 ParentId, 0 as LastUpdateTick union all
select 1545 as Id, '62.30.12' as Code, 'Запуск космических объектов в космическое пространство' as Name, 1542 ParentId, 0 as LastUpdateTick union all
select 1546 as Id, '62.30.2' as Code, 'Управление космическими объектами в космическом пространстве' as Name, 1542 ParentId, 0 as LastUpdateTick union all
select 1547 as Id, '63' as Code, 'Вспомогательная и дополнительная транспортная деятельность' as Name, 2428 ParentId, 0 as LastUpdateTick union all
select 1548 as Id, '63.1' as Code, 'Транспортная обработка грузов и хранение' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1549 as Id, '63.11' as Code, 'Транспортная обработка грузов' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1550 as Id, '63.11.1' as Code, 'Транспортная обработка контейнеров' as Name, 1549 ParentId, 0 as LastUpdateTick union all
select 1551 as Id, '63.11.2' as Code, 'Транспортная обработка прочих грузов' as Name, 1549 ParentId, 0 as LastUpdateTick union all
select 1552 as Id, '63.12' as Code, 'Хранение и складирование' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1553 as Id, '63.12.1' as Code, 'Хранение и складирование замороженных или охлажденных грузов' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1554 as Id, '63.12.2' as Code, 'Хранение и складирование жидких или газообразных грузов' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1555 as Id, '63.12.21' as Code, 'Хранение и складирование нефти и продуктов ее переработки' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1556 as Id, '63.12.22' as Code, 'Хранение и складирование газа и продуктов его переработки' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1557 as Id, '63.12.23' as Code, 'Хранение и складирование прочих жидких или газообразных грузов' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1558 as Id, '63.12.3' as Code, 'Хранение и складирование зерна' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1559 as Id, '63.12.4' as Code, 'Хранение и складирование прочих грузов' as Name, 1552 ParentId, 0 as LastUpdateTick union all
select 1560 as Id, '63.2' as Code, 'Прочая вспомогательная транспортная деятельность' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1561 as Id, '63.21' as Code, 'Прочая вспомогательная деятельность сухопутного транспорта' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1562 as Id, '63.21.1' as Code, 'Прочая вспомогательная деятельность железнодорожного транспорта' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 1563 as Id, '63.21.2' as Code, 'Прочая вспомогательная деятельность автомобильного транспорта' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 1564 as Id, '63.21.21' as Code, 'Деятельность терминалов (автобусных станций и т.п.)' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 1565 as Id, '63.21.22' as Code, 'Эксплуатация автомобильных дорог общего пользования' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 1566 as Id, '63.21.23' as Code, 'Эксплуатация дорожных сооружений (мостов, туннелей, путепроводов и  т.п.)' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 1567 as Id, '63.21.24' as Code, 'Эксплуатация гаражей, стоянок для автотранспортных средств, велосипедов и т.п.' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 1568 as Id, '63.22' as Code, 'Прочая вспомогательная деятельность водного транспорта' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1569 as Id, '63.22.1' as Code, 'Прочая вспомогательная деятельность морского транспорта' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 1570 as Id, '63.22.2' as Code, 'Прочая вспомогательная деятельность внутреннего водного транспорта' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 1571 as Id, '63.23' as Code, 'Прочая вспомогательная деятельность воздушного и космического транспорта' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1572 as Id, '63.23.1' as Code, 'Деятельность терминалов (аэропортов и т.п.), управление аэропортами' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 1573 as Id, '63.23.2' as Code, 'Управление воздушным движением' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 1574 as Id, '63.23.3' as Code, 'Эксплуатация взлетно-посадочных полос, ангаров и т.п.' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 1575 as Id, '63.23.4' as Code, 'Деятельность по наземному обслуживанию воздушных судов' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 1576 as Id, '63.23.5' as Code, 'Деятельность школ повышения квалификации (учебно-тренировочных  центров) для пилотов коммерческих авиалиний' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 1577 as Id, '63.23.6' as Code, 'Вспомогательная деятельность, связанная с использованием (эксплуатацией) космического транспорта' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 1578 as Id, '63.3' as Code, 'Деятельность туристических агентств' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1579 as Id, '63.30' as Code, 'Деятельность туристических агентств' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1580 as Id, '63.30.1' as Code, 'Организация комплексного туристического обслуживания' as Name, 1579 ParentId, 0 as LastUpdateTick union all
select 1581 as Id, '63.30.2' as Code, 'Обеспечение экскурсионными билетами, обеспечение проживания,  обеспечение транспортными средствами' as Name, 1579 ParentId, 0 as LastUpdateTick union all
select 1582 as Id, '63.30.3' as Code, 'Предоставление туристических информационных услуг' as Name, 1579 ParentId, 0 as LastUpdateTick union all
select 1583 as Id, '63.30.4' as Code, 'Предоставление туристических экскурсионных услуг' as Name, 1579 ParentId, 0 as LastUpdateTick union all
select 1584 as Id, '63.4' as Code, 'Организация перевозок грузов' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1585 as Id, '63.40' as Code, 'Организация перевозок грузов' as Name, 1547 ParentId, 0 as LastUpdateTick union all
select 1586 as Id, '64' as Code, 'Связь' as Name, 2428 ParentId, 0 as LastUpdateTick union all
select 1587 as Id, '64.1' as Code, 'Почтовая и курьерская деятельность' as Name, 1586 ParentId, 0 as LastUpdateTick union all
select 1588 as Id, '64.11' as Code, 'Деятельность национальной почты' as Name, 1586 ParentId, 0 as LastUpdateTick union all
select 1589 as Id, '64.11.1' as Code, 'Деятельность почтовой связи общего пользования' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1590 as Id, '64.11.11' as Code, 'Деятельность почтовой связи, связанная с пересылкой газет и других периодических изданий' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1591 as Id, '64.11.12' as Code, 'Деятельность почтовой связи, связанная с пересылкой письменной корреспонденции' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1592 as Id, '64.11.13' as Code, 'Деятельность почтовой связи, связанная с пересылкой посылочной почты' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1593 as Id, '64.11.14' as Code, 'Дополнительная деятельность почтовой связи' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1594 as Id, '64.11.2' as Code, 'Деятельность специальной почтовой связи' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1595 as Id, '64.11.3' as Code, 'Деятельность фельдъегерской связи' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1596 as Id, '64.11.31' as Code, 'Деятельность федеральной фельдъегерской связи' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1597 as Id, '64.11.32' as Code, 'Деятельность фельдъегерско-почтовой связи' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 1598 as Id, '64.12' as Code, 'Курьерская деятельность, кроме деятельности национальной почты' as Name, 1586 ParentId, 0 as LastUpdateTick union all
select 1599 as Id, '64.2' as Code, 'Деятельность в области электросвязи' as Name, 1586 ParentId, 0 as LastUpdateTick union all
select 1600 as Id, '64.20' as Code, 'Деятельность в области электросвязи' as Name, 1586 ParentId, 0 as LastUpdateTick union all
select 1601 as Id, '64.20.1' as Code, 'Деятельность в области телефонной связи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 1602 as Id, '64.20.11' as Code, 'Деятельность в области фиксированной телефонной связи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 1603 as Id, '64.20.12' as Code, 'Деятельность в области подвижной связи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 1604 as Id, '64.20.2' as Code, 'Деятельность в области телеграфной связи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 1605 as Id, '64.20.21' as Code, 'Деятельность в области передачи (трансляции) и распределения программ телевидения' as Name, 1604 ParentId, 0 as LastUpdateTick union all
select 1606 as Id, '64.20.22' as Code, 'Деятельность в области передачи (трансляции) и распределения программ звукового радиовещания' as Name, 1604 ParentId, 0 as LastUpdateTick union all
select 1607 as Id, '64.20.3' as Code, 'Деятельность в области оказания услуг межсистемной связи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 1608 as Id, '65' as Code, 'Финансовое посредничество' as Name, 2429 ParentId, 0 as LastUpdateTick union all
select 1609 as Id, '65.1' as Code, 'Денежное посредничество' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1610 as Id, '65.11' as Code, 'Деятельность Центрального банка Российской Федерации' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1611 as Id, '65.11.1' as Code, 'Разработка и проведение во взаимодействии с Правительством Российской Федерации единой государственной денежно-кредитной политики' as Name, 1610 ParentId, 0 as LastUpdateTick union all
select 1612 as Id, '65.11.11' as Code, 'Осуществление эмиссии наличных денег и организация наличного денежного обращения' as Name, 1610 ParentId, 0 as LastUpdateTick union all
select 1613 as Id, '65.11.12' as Code, 'Выполнение функции кредитора последней инстанции для кредитных организаций, организация системы их рефинансирования' as Name, 1610 ParentId, 0 as LastUpdateTick union all
select 1614 as Id, '65.11.9' as Code, 'Прочая деятельность Центрального банка Российской Федерации' as Name, 1610 ParentId, 0 as LastUpdateTick union all
select 1615 as Id, '65.12' as Code, 'Прочее денежное посредничество' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1616 as Id, '65.2' as Code, 'Прочее финансовое посредничество' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1617 as Id, '65.21' as Code, 'Финансовый лизинг' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1618 as Id, '65.22' as Code, 'Предоставление кредита' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1619 as Id, '65.22.1' as Code, 'Предоставление потребительского кредита' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 1620 as Id, '65.22.2' as Code, 'Предоставление займов промышленности' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 1621 as Id, '65.22.3' as Code, 'Предоставление денежных ссуд под залог недвижимого имущества' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 1622 as Id, '65.22.4' as Code, 'Предоставление кредитов на покупку домов специализированными учреждениями, не принимающими депозиты' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 1623 as Id, '65.22.5' as Code, 'Предоставление услуг по обеспечению кредитных карточек' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 1624 as Id, '65.22.6' as Code, 'Предоставление ломбардами краткосрочных кредитов под залог движимого имущества' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 1625 as Id, '65.23' as Code, 'Финансовое посредничество, не включенное в другие группировки' as Name, 1608 ParentId, 0 as LastUpdateTick union all
select 1626 as Id, '65.23.1' as Code, 'Капиталовложения в ценные бумаги' as Name, 1625 ParentId, 0 as LastUpdateTick union all
select 1627 as Id, '65.23.2' as Code, 'Деятельность дилеров' as Name, 1625 ParentId, 0 as LastUpdateTick union all
select 1628 as Id, '65.23.3' as Code, 'Капиталовложения в собственность' as Name, 1625 ParentId, 0 as LastUpdateTick union all
select 1629 as Id, '65.23.4' as Code, 'Заключение свопов, опционов и других биржевых сделок' as Name, 1625 ParentId, 0 as LastUpdateTick union all
select 1630 as Id, '65.23.5' as Code, 'Деятельность холдинг-компаний в области финансового посредничества' as Name, 1625 ParentId, 0 as LastUpdateTick union all
select 1631 as Id, '66' as Code, 'Страхование' as Name, 2429 ParentId, 0 as LastUpdateTick union all
select 1632 as Id, '66.0' as Code, 'Страхование' as Name, 1631 ParentId, 0 as LastUpdateTick union all
select 1633 as Id, '66.01' as Code, 'Страхование жизни' as Name, 1631 ParentId, 0 as LastUpdateTick union all
select 1634 as Id, '66.02' as Code, 'Добровольное пенсионное страхование' as Name, 1631 ParentId, 0 as LastUpdateTick union all
select 1635 as Id, '66.02.1' as Code, 'Деятельность по добровольному пенсионному страхованию' as Name, 1634 ParentId, 0 as LastUpdateTick union all
select 1636 as Id, '66.02.2' as Code, 'Страхование ренты' as Name, 1634 ParentId, 0 as LastUpdateTick union all
select 1637 as Id, '66.03' as Code, 'Прочие виды страхования' as Name, 1631 ParentId, 0 as LastUpdateTick union all
select 1638 as Id, '66.03.1' as Code, 'Добровольное медицинское страхование' as Name, 1637 ParentId, 0 as LastUpdateTick union all
select 1639 as Id, '66.03.2' as Code, 'Страхование имущества' as Name, 1637 ParentId, 0 as LastUpdateTick union all
select 1640 as Id, '66.03.3' as Code, 'Страхование ответственности' as Name, 1637 ParentId, 0 as LastUpdateTick union all
select 1641 as Id, '66.03.4' as Code, 'Страхование от несчастных случаев и болезней' as Name, 1637 ParentId, 0 as LastUpdateTick union all
select 1642 as Id, '66.03.5' as Code, 'Страхование рисков' as Name, 1637 ParentId, 0 as LastUpdateTick union all
select 1643 as Id, '66.03.9' as Code, 'Прочие виды страхования, не включенные в другие группировки' as Name, 1637 ParentId, 0 as LastUpdateTick union all
select 1644 as Id, '67' as Code, 'Вспомогательная деятельность в сфере финансового посредничества и страхования' as Name, 2429 ParentId, 0 as LastUpdateTick union all
select 1645 as Id, '67.1' as Code, 'Вспомогательная деятельность в сфере финансового посредничества' as Name, 1644 ParentId, 0 as LastUpdateTick union all
select 1646 as Id, '67.11' as Code, 'Управление финансовыми рынками' as Name, 1644 ParentId, 0 as LastUpdateTick union all
select 1647 as Id, '67.11.1' as Code, 'Деятельность фондовых, товарных, валютных и валютно-фондовых бирж' as Name, 1646 ParentId, 0 as LastUpdateTick union all
select 1648 as Id, '67.11.11' as Code, 'Деятельность по организации торговли на финансовых рынках' as Name, 1646 ParentId, 0 as LastUpdateTick union all
select 1649 as Id, '67.11.12' as Code, 'Деятельность по ведению реестра владельцев ценных бумаг (деятельность регистраторов)' as Name, 1646 ParentId, 0 as LastUpdateTick union all
select 1650 as Id, '67.11.13' as Code, 'Деятельность по обеспечению эффективности функционирования финансовых рынков' as Name, 1646 ParentId, 0 as LastUpdateTick union all
select 1651 as Id, '67.11.19' as Code, 'Прочая деятельность, связанная с управлением финансовыми рынками, не включенная в другие группировки' as Name, 1646 ParentId, 0 as LastUpdateTick union all
select 1652 as Id, '67.12' as Code, 'Биржевые операции с фондовыми ценностями и управление активами' as Name, 1644 ParentId, 0 as LastUpdateTick union all
select 1653 as Id, '67.12.1' as Code, 'Брокерская деятельность' as Name, 1652 ParentId, 0 as LastUpdateTick union all
select 1654 as Id, '67.12.2' as Code, 'Деятельность по управлению ценными бумагами' as Name, 1652 ParentId, 0 as LastUpdateTick union all
select 1655 as Id, '67.12.3' as Code, 'Деятельность по определению взаимных обязательств (клиринг)' as Name, 1652 ParentId, 0 as LastUpdateTick union all
select 1656 as Id, '67.12.4' as Code, 'Эмиссионная деятельность' as Name, 1652 ParentId, 0 as LastUpdateTick union all
select 1657 as Id, '67.13' as Code, 'Прочая вспомогательная деятельность в сфере финансового посредничества' as Name, 1644 ParentId, 0 as LastUpdateTick union all
select 1658 as Id, '67.13.1' as Code, 'Предоставление брокерских услуг по ипотечным операциям' as Name, 1657 ParentId, 0 as LastUpdateTick union all
select 1659 as Id, '67.13.2' as Code, 'Предоставление услуг пунктами по обмену валют' as Name, 1657 ParentId, 0 as LastUpdateTick union all
select 1660 as Id, '67.13.3' as Code, 'Предоставление услуг по упаковыванию денег' as Name, 1657 ParentId, 0 as LastUpdateTick union all
select 1661 as Id, '67.13.4' as Code, 'Консультирование по вопросам финансового посредничества' as Name, 1657 ParentId, 0 as LastUpdateTick union all
select 1662 as Id, '67.13.5' as Code, 'Предоставление услуг по хранению ценностей' as Name, 1657 ParentId, 0 as LastUpdateTick union all
select 1663 as Id, '67.13.51' as Code, 'Депозитарная деятельность' as Name, 1657 ParentId, 0 as LastUpdateTick union all
select 1664 as Id, '67.2' as Code, 'Вспомогательная деятельность в сфере страхования и негосударственного  пенсионного обеспечения' as Name, 1644 ParentId, 0 as LastUpdateTick union all
select 1665 as Id, '67.20' as Code, 'Вспомогательная деятельность в сфере страхования и негосударственного пенсионного обеспечения' as Name, 1644 ParentId, 0 as LastUpdateTick union all
select 1666 as Id, '67.20.1' as Code, 'Деятельность страховых агентов' as Name, 1665 ParentId, 0 as LastUpdateTick union all
select 1667 as Id, '67.20.2' as Code, 'Деятельность специалистов по оценке страхового риска и убытков' as Name, 1665 ParentId, 0 as LastUpdateTick union all
select 1668 as Id, '67.20.3' as Code, 'Деятельность специалистов по расчетам оценки страховой вероятности (актуариев)' as Name, 1665 ParentId, 0 as LastUpdateTick union all
select 1669 as Id, '67.20.4' as Code, 'Деятельность распорядителей спасательными работами' as Name, 1665 ParentId, 0 as LastUpdateTick union all
select 1670 as Id, '67.20.9' as Code, 'Прочая вспомогательная деятельность в сфере страхования, кроме обязательного социального страхования' as Name, 1665 ParentId, 0 as LastUpdateTick union all
select 1671 as Id, '70' as Code, 'Операции с недвижимым имуществом' as Name, 2430 ParentId, 0 as LastUpdateTick union all
select 1672 as Id, '70.1' as Code, 'Подготовка к продаже, покупка и продажа собственного недвижимого  имущества' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1673 as Id, '70.11' as Code, 'Подготовка к продаже собственного недвижимого имущества' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1674 as Id, '70.11.1' as Code, 'Подготовка к продаже собственного жилого недвижимого имущества' as Name, 1673 ParentId, 0 as LastUpdateTick union all
select 1675 as Id, '70.11.2' as Code, 'Подготовка к продаже собственного нежилого недвижимого имущества' as Name, 1673 ParentId, 0 as LastUpdateTick union all
select 1676 as Id, '70.12' as Code, 'Покупка и продажа собственного недвижимого имущества' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1677 as Id, '70.12.1' as Code, 'Покупка и продажа собственного жилого недвижимого имущества' as Name, 1676 ParentId, 0 as LastUpdateTick union all
select 1678 as Id, '70.12.2' as Code, 'Покупка и продажа собственных нежилых зданий и помещений' as Name, 1676 ParentId, 0 as LastUpdateTick union all
select 1679 as Id, '70.12.3' as Code, 'Покупка и продажа земельных участков' as Name, 1676 ParentId, 0 as LastUpdateTick union all
select 1680 as Id, '70.2' as Code, 'Сдача внаем собственного недвижимого имущества' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1681 as Id, '70.20' as Code, 'Сдача внаем собственного недвижимого имущества' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1682 as Id, '70.20.1' as Code, 'Сдача внаем собственного жилого недвижимого имущества' as Name, 1681 ParentId, 0 as LastUpdateTick union all
select 1683 as Id, '70.20.2' as Code, 'Сдача внаем собственного нежилого недвижимого имущества' as Name, 1681 ParentId, 0 as LastUpdateTick union all
select 1684 as Id, '70.3' as Code, 'Предоставление посреднических услуг, связанных с недвижимым  имуществом' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1685 as Id, '70.31' as Code, 'Деятельность агентств по операциям с недвижимым имуществом' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1686 as Id, '70.31.1' as Code, 'Предоставление посреднических услуг при покупке, продаже и аренде недвижимого имущества' as Name, 1685 ParentId, 0 as LastUpdateTick union all
select 1687 as Id, '70.31.11' as Code, 'Предоставление посреднических услуг при покупке, продаже и аренде жилого недвижимого имущества' as Name, 1685 ParentId, 0 as LastUpdateTick union all
select 1688 as Id, '70.31.12' as Code, 'Предоставление посреднических услуг при покупке, продаже и аренде нежилого недвижимого имущества' as Name, 1685 ParentId, 0 as LastUpdateTick union all
select 1689 as Id, '70.31.2' as Code, 'Предоставление посреднических услуг при оценке недвижимого имущества' as Name, 1685 ParentId, 0 as LastUpdateTick union all
select 1690 as Id, '70.31.21' as Code, 'Предоставление посреднических услуг при оценке жилого недвижимого имущества' as Name, 1685 ParentId, 0 as LastUpdateTick union all
select 1691 as Id, '70.31.22' as Code, 'Предоставление посреднических услуг при оценке нежилого недвижимого имущества' as Name, 1685 ParentId, 0 as LastUpdateTick union all
select 1692 as Id, '70.32' as Code, 'Управление недвижимым имуществом' as Name, 1671 ParentId, 0 as LastUpdateTick union all
select 1693 as Id, '70.32.1' as Code, 'Управление эксплуатацией жилого фонда' as Name, 1692 ParentId, 0 as LastUpdateTick union all
select 1694 as Id, '70.32.2' as Code, 'Управление эксплуатацией нежилого фонда' as Name, 1692 ParentId, 0 as LastUpdateTick union all
select 1695 as Id, '70.32.3' as Code, 'Деятельность по учету и технической инвентаризации недвижимого имущества' as Name, 1692 ParentId, 0 as LastUpdateTick union all
select 1696 as Id, '71' as Code, 'Аренда машин и оборудования без оператора; прокат бытовых изделий и предметов личного пользования' as Name, 2430 ParentId, 0 as LastUpdateTick union all
select 1697 as Id, '71.1' as Code, 'Аренда легковых автомобилей' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1698 as Id, '71.10' as Code, 'Аренда легковых автомобилей' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1699 as Id, '71.2' as Code, 'Аренда прочих транспортных средств и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1700 as Id, '71.21' as Code, 'Аренда прочих сухопутных транспортных средств и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1701 as Id, '71.21.1' as Code, 'Аренда прочего автомобильного транспорта и оборудования' as Name, 1700 ParentId, 0 as LastUpdateTick union all
select 1702 as Id, '71.21.2' as Code, 'Аренда железнодорожного транспорта и оборудования' as Name, 1700 ParentId, 0 as LastUpdateTick union all
select 1703 as Id, '71.22' as Code, 'Аренда водных транспортных средств и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1704 as Id, '71.23' as Code, 'Аренда воздушных транспортных средств и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1705 as Id, '71.3' as Code, 'Аренда прочих машин и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1706 as Id, '71.31' as Code, 'Аренда сельскохозяйственных машин и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1707 as Id, '71.32' as Code, 'Аренда строительных машин и оборудования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1708 as Id, '71.33' as Code, 'Аренда офисных машин и оборудования, включая вычислительную технику' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1709 as Id, '71.33.1' as Code, 'Аренда офисных машин и оборудования' as Name, 1708 ParentId, 0 as LastUpdateTick union all
select 1710 as Id, '71.33.2' as Code, 'Аренда вычислительных машин и оборудования' as Name, 1708 ParentId, 0 as LastUpdateTick union all
select 1711 as Id, '71.34' as Code, 'Аренда прочих машин и оборудования, не включенных в другие группировки' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1712 as Id, '71.34.1' as Code, 'Аренда двигателей, турбин и станков' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1713 as Id, '71.34.2' as Code, 'Аренда горного и нефтепромыслового оборудования' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1714 as Id, '71.34.3' as Code, 'Аренда подъемно-транспортного оборудования' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1715 as Id, '71.34.4' as Code, 'Аренда профессиональной радио- и телевизионной аппаратуры и аппаратуры связи' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1716 as Id, '71.34.5' as Code, 'Аренда контрольно-измерительной аппаратуры' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1717 as Id, '71.34.6' as Code, 'Аренда медицинской техники' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1718 as Id, '71.34.7' as Code, 'Аренда торгового оборудования' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1719 as Id, '71.34.9' as Code, 'Аренда прочих машин и оборудования научного и промышленного назначения' as Name, 1711 ParentId, 0 as LastUpdateTick union all
select 1720 as Id, '71.4' as Code, 'Прокат бытовых изделий и предметов личного пользования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1721 as Id, '71.40' as Code, 'Прокат бытовых изделий и предметов личного пользования' as Name, 1696 ParentId, 0 as LastUpdateTick union all
select 1722 as Id, '71.40.1' as Code, 'Прокат телевизоров, радиоприемников, устройств видеозаписи, аудиозаписи и подобного оборудования' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1723 as Id, '71.40.2' as Code, 'Прокат аудио- и видеокассет, грампластинок и записей на других технических носителях информации' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1724 as Id, '71.40.3' as Code, 'Прокат мебели, электрических и неэлектрических бытовых приборов' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1725 as Id, '71.40.4' as Code, 'Прокат инвентаря и оборудования для проведения досуга и отдыха' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1726 as Id, '71.40.5' as Code, 'Прокат музыкальных инструментов' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1727 as Id, '71.40.6' as Code, 'Прокат предметов медицинского и санитарного обслуживания' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1728 as Id, '71.40.9' as Code, 'Прокат прочих бытовых изделий и предметов личного пользования для домашних хозяйств, предприятий и организаций, не включенных в другие группировки' as Name, 1721 ParentId, 0 as LastUpdateTick union all
select 1729 as Id, '72' as Code, 'Деятельность, связанная с использованием вычислительной техники и информационных технологий' as Name, 2430 ParentId, 0 as LastUpdateTick union all
select 1730 as Id, '72.1' as Code, 'Консультирование по аппаратным средствам вычислительной техники' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1731 as Id, '72.10' as Code, 'Консультирование по аппаратным средствам вычислительной техники' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1732 as Id, '72.2' as Code, 'Разработка программного обеспечения и консультирование в этой области' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1733 as Id, '72.20' as Code, 'Разработка программного обеспечения и консультирование в этой области' as Name, 1732 ParentId, 0 as LastUpdateTick union all
select 1734 as Id, '72.3' as Code, 'Обработка данных' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1735 as Id, '72.30' as Code, 'Обработка данных' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1736 as Id, '72.4' as Code, 'Деятельность по созданию и использованию баз данных и информационных ресурсов, в том числе ресурсов сети Интернет' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1737 as Id, '72.40' as Code, 'Деятельность по созданию и использованию баз данных и информационных ресурсов, в том числе ресурсов сети Интернет' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1738 as Id, '72.5' as Code, 'Техническое обслуживание и ремонт офисных машин и вычислительной техники' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1739 as Id, '72.50' as Code, 'Техническое обслуживание и ремонт офисных машин и вычислительной техники' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1740 as Id, '72.6' as Code, 'Прочая деятельность, связанная с использованием вычислительной техники и информационных технологий' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1741 as Id, '72.60' as Code, 'Прочая деятельность, связанная с использованием вычислительной техники и информационных технологий' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 1742 as Id, '73' as Code, 'Научные исследования и разработки' as Name, 2430 ParentId, 0 as LastUpdateTick union all
select 1743 as Id, '73.1' as Code, 'Научные исследования и разработки в области естественных и технических наук' as Name, 1742 ParentId, 0 as LastUpdateTick union all
select 1744 as Id, '73.10' as Code, 'Научные исследования и разработки в области естественных и технических наук' as Name, 1742 ParentId, 0 as LastUpdateTick union all
select 1745 as Id, '73.2' as Code, 'Научные исследования и разработки в области общественных и гуманитарных наук' as Name, 1742 ParentId, 0 as LastUpdateTick union all
select 1746 as Id, '73.20' as Code, 'Научные исследования и разработки в области общественных и гуманитарных наук' as Name, 1742 ParentId, 0 as LastUpdateTick union all
select 1747 as Id, '74' as Code, 'Предоставление прочих видов услуг' as Name, 2430 ParentId, 0 as LastUpdateTick union all
select 1748 as Id, '74.1' as Code, 'Деятельность в области права, бухгалтерского учета и аудита; консультирование по вопросам коммерческой деятельности и управления предприятием' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1749 as Id, '74.11' as Code, 'Деятельность в области права' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1750 as Id, '74.12' as Code, 'Деятельность в области бухгалтерского учета и аудита' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1751 as Id, '74.12.1' as Code, 'Деятельность в области бухгалтерского учета' as Name, 1750 ParentId, 0 as LastUpdateTick union all
select 1752 as Id, '74.12.2' as Code, 'Аудиторская деятельность' as Name, 1750 ParentId, 0 as LastUpdateTick union all
select 1753 as Id, '74.13' as Code, 'Маркетинговые исследования и выявление общественного мнения' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1754 as Id, '74.13.1' as Code, 'Маркетинговые исследования' as Name, 1753 ParentId, 0 as LastUpdateTick union all
select 1755 as Id, '74.13.2' as Code, 'Деятельность по изучению общественного мнения' as Name, 1753 ParentId, 0 as LastUpdateTick union all
select 1756 as Id, '74.14' as Code, 'Консультирование по вопросам коммерческой деятельности и управления' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1757 as Id, '74.15' as Code, 'Деятельность по управлению финансово-промышленными группами и холдинг-компаниями' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1758 as Id, '74.15.1' as Code, 'Деятельность по управлению финансово-промышленными группами' as Name, 1757 ParentId, 0 as LastUpdateTick union all
select 1759 as Id, '74.15.2' as Code, 'Деятельность по управлению холдинг-компаниями' as Name, 1757 ParentId, 0 as LastUpdateTick union all
select 1760 as Id, '74.2' as Code, 'Деятельность в области архитектуры; инженерно-техническое проектирование; геолого-разведочные и геофизические работы; геодезическая и картографическая деятельность; деятельность в области стандартизации и метрологии; деятельность в области гидрометеорологии и смежных с ней областях; виды деятельности, связанные с решением технических задач, не включенные в другие группировки' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1761 as Id, '74.20' as Code, 'Деятельность в области архитектуры; инженерно-техническое проектирование; геолого-разведочные и геофизические работы; геодезическая и картографическая деятельность; деятельность в области стандартизации и метрологии; деятельность в области гидрометеорологии и смежных с ней областях, мониторинга состояния окружающей среды, ее загрязнения; виды деятельности, связанные с решением технических задач, не включенные в другие группировки' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1762 as Id, '74.20.1' as Code, 'Деятельность в области архитектуры, инженерно-техническое проектирование в промышленности и строительстве' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1763 as Id, '74.20.11' as Code, 'Архитектурная деятельность' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1764 as Id, '74.20.12' as Code, 'Проектирование производственных помещений, включая размещение машин и оборудования' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1765 as Id, '74.20.13' as Code, 'Проектирование, связанное со строительством инженерных сооружений, включая гидротехнические сооружения; проектирование движения транспортных потоков' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1766 as Id, '74.20.14' as Code, 'Разработка проектов промышленных процессов и производств, относящихся к электротехнике, электронной технике, горному делу, химической  технологии, машиностроению, а также в области промышленного строительства, системотехники и техники безопасности' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1767 as Id, '74.20.15' as Code, 'Разработка проектов в области кондиционирования воздуха, холодильной техники, санитарной техники и мониторинга загрязнения окружающей среды, строительной акустики и т.п.' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1768 as Id, '74.20.2' as Code, 'Геолого-разведочные, геофизические и геохимические работы в области изучения недр и воспроизводства минерально-сырьевой базы' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1769 as Id, '74.20.3' as Code, 'Геодезическая и картографическая деятельность' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1770 as Id, '74.20.31' as Code, 'Топографо-геодезическая деятельность' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1771 as Id, '74.20.32' as Code, 'Картографическая деятельность, включая деятельность в областях наименований географических объектов и создания и ведения картографо-геодезического фонда' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1772 as Id, '74.20.33' as Code, 'Гидрографические изыскательские работы' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1773 as Id, '74.20.34' as Code, 'Деятельность, связанная со сбором, обработкой и подготовкой картографической и космической информации, включая аэросъемку' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1774 as Id, '74.20.35' as Code, 'Инженерные изыскания для строительства' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1775 as Id, '74.20.36' as Code, 'Землеустройство' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1776 as Id, '74.20.4' as Code, 'Деятельность в области стандартизации и метрологии' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1777 as Id, '74.20.41' as Code, 'Деятельность в области стандартизации' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1778 as Id, '74.20.42' as Code, 'Деятельность в области метрологии' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1779 as Id, '74.20.44' as Code, 'Деятельность в области аккредитации' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1780 as Id, '74.20.45' as Code, 'Государственный контроль и надзор за стандартами, средствами измерений и обязательной сертификацией' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1781 as Id, '74.20.5' as Code, 'Деятельность в области гидрометеорологии и смежных с ней областях, мониторинга состояния окружающей среды, ее загрязнения' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1782 as Id, '74.20.51' as Code, 'Деятельность наблюдательной гидрометеорологической сети' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1783 as Id, '74.20.52' as Code, 'Проведение гелиофизических и геофизических работ' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1784 as Id, '74.20.53' as Code, 'Деятельность по мониторингу загрязнения окружающей среды для физических и юридических лиц' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1785 as Id, '74.20.54' as Code, 'Полевые работы и изыскания в области гидрометеорологии и смежных с ней областях, экспедиционные обследования объектов окружающей среды с целью оценки уровней загрязнения' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1786 as Id, '74.20.55' as Code, 'Деятельность по обработке и предоставлению гидрометеорологической информации органам государственной власти и населению' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1787 as Id, '74.20.56' as Code, 'Гидрометеорологическое обеспечение деятельности физических и юридических лиц' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 1788 as Id, '74.3' as Code, 'Технические испытания, исследования и сертификация' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1789 as Id, '74.30' as Code, 'Технические испытания, исследования и сертификация' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1790 as Id, '74.30.1' as Code, 'Испытания и анализ состава и чистоты материалов и веществ: анализ химических и биологических свойств материалов и веществ (воздуха, воды, бытовых и производственных отходов, топлива, металла, почвы, химических веществ)' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1791 as Id, '74.30.2' as Code, 'Ветеринарный контроль и контроль за производством продуктов питания' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1792 as Id, '74.30.3' as Code, 'Испытания и анализ в научных областях (микробиологии, биохимии, бактериологии и др.)' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1793 as Id, '74.30.4' as Code, 'Испытания и анализ физических свойств материалов и веществ: испытания и анализ физических свойств (прочности, пластичности, электропроводности, радиоактивности) материалов (металлов, пластмасс, тканей, дерева, стекла, бетона и др.); испытания на растяжение, твердость, сопротивление, усталость и высокотемпературный эффект' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1794 as Id, '74.30.5' as Code, 'Испытания и анализ механических и электрических характеристик готовой продукции: моторов, автомобилей, станков, радиоэлектронных устройств, оборудования связи и другого оборудования, включающего механические и электрические компоненты' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1795 as Id, '74.30.6' as Code, 'Испытания и расчеты строительных элементов' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1796 as Id, '74.30.7' as Code, 'Технический контроль автомобилей: периодический технический осмотр легковых и грузовых автомобилей, мотоциклов, автобусов и других автотранспортных средств' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1797 as Id, '74.30.8' as Code, 'Сертификация продукции и услуг' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1798 as Id, '74.30.9' as Code, 'Прочая деятельность по техническому контролю, испытаниям и анализу' as Name, 1789 ParentId, 0 as LastUpdateTick union all
select 1799 as Id, '74.4' as Code, 'Рекламная деятельность' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1800 as Id, '74.40' as Code, 'Рекламная деятельность' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1801 as Id, '74.5' as Code, 'Трудоустройство и подбор персонала' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1802 as Id, '74.50' as Code, 'Трудоустройство и подбор персонала' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1803 as Id, '74.50.1' as Code, 'Предоставление услуг по трудоустройству' as Name, 1802 ParentId, 0 as LastUpdateTick union all
select 1804 as Id, '74.50.2' as Code, 'Предоставление услуг по подбору персонала' as Name, 1802 ParentId, 0 as LastUpdateTick union all
select 1805 as Id, '74.6' as Code, 'Проведение расследований и обеспечение безопасности' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1806 as Id, '74.60' as Code, 'Проведение расследований и обеспечение безопасности' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1807 as Id, '74.7' as Code, 'Чистка и уборка производственных и жилых помещений, оборудования и  транспортных средств' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1808 as Id, '74.70' as Code, 'Чистка и уборка производственных и жилых помещений, оборудования и транспортных средств' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1809 as Id, '74.70.1' as Code, 'Чистка и уборка производственных и жилых помещений и оборудования' as Name, 1808 ParentId, 0 as LastUpdateTick union all
select 1810 as Id, '74.70.2' as Code, 'Чистка и уборка транспортных средств' as Name, 1808 ParentId, 0 as LastUpdateTick union all
select 1811 as Id, '74.70.3' as Code, 'Деятельность по проведению дезинфекционных, дезинсекционных и дератизационных работ' as Name, 1808 ParentId, 0 as LastUpdateTick union all
select 1812 as Id, '74.8' as Code, 'Предоставление различных видов услуг' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1813 as Id, '74.81' as Code, 'Деятельность в области фотографии' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1814 as Id, '74.82' as Code, 'Упаковывание' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 1815 as Id, '74.83' as Code, 'Предоставление секретарских, редакторских услуг и услуг по переводу' as Name, 1812 ParentId, 0 as LastUpdateTick union all
select 1816 as Id, '74.84' as Code, 'Предоставление прочих услуг' as Name, 1812 ParentId, 0 as LastUpdateTick union all
select 1817 as Id, '75' as Code, 'Государственное управление и обеспечение военной безопасности; социальное страхование' as Name, 2431 ParentId, 0 as LastUpdateTick union all
select 1818 as Id, '75.1' as Code, 'Государственное управление общего и социально-экономического характера' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1819 as Id, '75.11' as Code, 'Государственное управление общего характера' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1820 as Id, '75.11.1' as Code, 'Деятельность федеральных органов государственной власти по управлению вопросами общего характера, кроме судебной власти' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1821 as Id, '75.11.11' as Code, 'Деятельность федеральных органов государственной власти, кроме полномочных представителей Президента Российской Федерации и территориальных органов федеральных органов исполнительной власти' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1822 as Id, '75.11.12' as Code, 'Деятельность полномочных представителей Президента Российской Федерации в регионах Российской Федерации и территориальных органов федеральных органов исполнительной власти в субъектах Российской Федерации (республиках, краях, областях)' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1823 as Id, '75.11.13' as Code, 'Деятельность территориальных органов федеральных органов исполнительной власти в городах и районах субъектов Российской Федерации' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1824 as Id, '75.11.2' as Code, 'Деятельность органов государственной власти по управлению вопросами общего характера, кроме судебной власти, субъектов Российской Федерации' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1825 as Id, '75.11.21' as Code, 'Деятельность органов государственной власти субъектов (республик, краев, областей), кроме судебной власти, представительств субъектов Российской Федерации при Президенте Российской Федерации' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1826 as Id, '75.11.22' as Code, 'Деятельность органов государственной власти субъектов Российской Федерации, осуществляющих свои полномочия в городах и районах' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1827 as Id, '75.11.23' as Code, 'Деятельность органов государственной власти субъектов Российской Федерации, осуществляющих свои полномочия в сельских населенных пунктах' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1828 as Id, '75.11.3' as Code, 'Деятельность органов местного самоуправления по управлению вопросами общего характера' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1829 as Id, '75.11.31' as Code, 'Деятельность органов местного самоуправления муниципальных районов' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1830 as Id, '75.11.32' as Code, 'Деятельность органов местного самоуправления городских округов' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1831 as Id, '75.11.4' as Code, 'Управление финансовой и фискальной деятельностью' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1832 as Id, '75.11.5' as Code, 'Управление деятельностью в области прогнозирования и планирования' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1833 as Id, '75.11.6' as Code, 'Управление деятельностью в области фундаментальных исследований' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1834 as Id, '75.11.7' as Code, 'Управление деятельностью в области статистики и социологии' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1835 as Id, '75.11.8' as Code, 'Управление имуществом, находящимся в государственной собственности' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 1836 as Id, '75.12' as Code, 'Государственное управление социальными программами' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1837 as Id, '75.13' as Code, 'Регулирование и содействие эффективному ведению экономической деятельности, деятельность в области региональной, национальной и молодежной политики' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1838 as Id, '75.14' as Code, 'Вспомогательная деятельность в области государственного управления' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1839 as Id, '75.2' as Code, 'Предоставление государством услуг обществу в целом' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1840 as Id, '75.21' as Code, 'Международная деятельность' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1841 as Id, '75.22' as Code, 'Деятельность, связанная с обеспечением военной безопасности' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1842 as Id, '75.23' as Code, 'Деятельность в области юстиции и правосудия' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1843 as Id, '75.23.1' as Code, 'Деятельность Федеральных судов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1844 as Id, '75.23.11' as Code, 'Деятельность Конституционного суда Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1845 as Id, '75.23.12' as Code, 'Деятельность Верховного суда Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1846 as Id, '75.23.13' as Code, 'Деятельность Верховных судов субъектов Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1847 as Id, '75.23.14' as Code, 'Деятельность районных судов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1848 as Id, '75.23.15' as Code, 'Деятельность военных судов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1849 as Id, '75.23.16' as Code, 'Деятельность Высшего арбитражного суда Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1850 as Id, '75.23.17' as Code, 'Деятельность Федеральных арбитражных судов округов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1851 as Id, '75.23.18' as Code, 'Деятельность арбитражных судов субъектов Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1852 as Id, '75.23.19' as Code, 'Деятельность специализированных судов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1853 as Id, '75.23.2' as Code, 'Деятельность судов субъектов Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1854 as Id, '75.23.21' as Code, 'Деятельность конституционных (уставных) судов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1855 as Id, '75.23.22' as Code, 'Деятельность мировых судей' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1856 as Id, '75.23.3' as Code, 'Деятельность органов прокуратуры Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1857 as Id, '75.23.31' as Code, 'Деятельность Генеральной прокуратуры Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1858 as Id, '75.23.32' as Code, 'Деятельность прокуратур субъектов Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1859 as Id, '75.23.33' as Code, 'Деятельность прокуратур городов и районов' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1860 as Id, '75.23.4' as Code, 'Деятельность по управлению и эксплуатации тюрем, исправительных колоний и других мест лишения свободы, а также по оказанию реабилитационной помощи бывшим заключенным' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 1861 as Id, '75.24' as Code, 'Деятельность по обеспечению общественного порядка и безопасности' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1862 as Id, '75.24.1' as Code, 'Деятельность органов внутренних дел' as Name, 1861 ParentId, 0 as LastUpdateTick union all
select 1863 as Id, '75.24.2' as Code, 'Деятельность специализированных государственных органов охраны и безопасности' as Name, 1861 ParentId, 0 as LastUpdateTick union all
select 1864 as Id, '75.25' as Code, 'Деятельность по обеспечению безопасности в чрезвычайных ситуациях' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1865 as Id, '75.25.1' as Code, 'Деятельность по обеспечению пожарной безопасности' as Name, 1864 ParentId, 0 as LastUpdateTick union all
select 1866 as Id, '75.25.2' as Code, 'Деятельность по обеспечению безопасности на водных объектах' as Name, 1864 ParentId, 0 as LastUpdateTick union all
select 1867 as Id, '75.3' as Code, 'Деятельность в области обязательного социального страхования' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1868 as Id, '75.30' as Code, 'Деятельность в области обязательного социального страхования' as Name, 1817 ParentId, 0 as LastUpdateTick union all
select 1869 as Id, '80' as Code, 'Образование' as Name, 2432 ParentId, 0 as LastUpdateTick union all
select 1870 as Id, '80.1' as Code, 'Дошкольное и начальное общее образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1871 as Id, '80.10' as Code, 'Дошкольное и начальное общее образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1872 as Id, '80.10.1' as Code, 'Дошкольное образование (предшествующее начальному общему образованию)' as Name, 1871 ParentId, 0 as LastUpdateTick union all
select 1873 as Id, '80.10.2' as Code, 'Начальное общее образование' as Name, 1871 ParentId, 0 as LastUpdateTick union all
select 1874 as Id, '80.10.3' as Code, 'Дополнительное образование детей' as Name, 1871 ParentId, 0 as LastUpdateTick union all
select 1875 as Id, '80.2' as Code, 'Основное общее, среднее (полное) общее, начальное профессиональное образование и среднее профессиональное образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1876 as Id, '80.21' as Code, 'Основное общее и среднее (полное) общее образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1877 as Id, '80.21.1' as Code, 'Основное общее образование' as Name, 1876 ParentId, 0 as LastUpdateTick union all
select 1878 as Id, '80.21.2' as Code, 'Среднее (полное) общее образование' as Name, 1876 ParentId, 0 as LastUpdateTick union all
select 1879 as Id, '80.22' as Code, 'Начальное и среднее профессиональное образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1880 as Id, '80.22.1' as Code, 'Начальное профессиональное образование' as Name, 1879 ParentId, 0 as LastUpdateTick union all
select 1881 as Id, '80.22.2' as Code, 'Среднее профессиональное образование' as Name, 1879 ParentId, 0 as LastUpdateTick union all
select 1882 as Id, '80.22.21' as Code, 'Обучение в образовательных учреждениях среднего профессионального образования' as Name, 1879 ParentId, 0 as LastUpdateTick union all
select 1883 as Id, '80.22.22' as Code, 'Обучение в образовательных учреждениях дополнительного профессионального образования (повышения квалификации) для специалистов, имеющих среднее профессиональное образование' as Name, 1879 ParentId, 0 as LastUpdateTick union all
select 1884 as Id, '80.22.23' as Code, 'Обучение на подготовительных курсах для поступления в образовательные учреждения среднего профессионального образования' as Name, 1879 ParentId, 0 as LastUpdateTick union all
select 1885 as Id, '80.3' as Code, 'Высшее профессиональное образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1886 as Id, '80.30' as Code, 'Высшее профессиональное образование' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1887 as Id, '80.30.1' as Code, 'Обучение в образовательных учреждениях высшего профессионального образования (университетах, академиях, институтах и в др.)' as Name, 1886 ParentId, 0 as LastUpdateTick union all
select 1888 as Id, '80.30.2' as Code, 'Послевузовское профессиональное образование' as Name, 1886 ParentId, 0 as LastUpdateTick union all
select 1889 as Id, '80.30.3' as Code, 'Обучение в образовательных учреждениях дополнительного профессионального образования (повышения квалификации) для специалистов, имеющих высшее профессиональное образование' as Name, 1886 ParentId, 0 as LastUpdateTick union all
select 1890 as Id, '80.30.4' as Code, 'Обучение на подготовительных курсах для поступления в учебные заведения высшего профессионального образования' as Name, 1886 ParentId, 0 as LastUpdateTick union all
select 1891 as Id, '80.4' as Code, 'Образование для взрослых и прочие виды образования' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1892 as Id, '80.41' as Code, 'Обучение водителей транспортных средств' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1893 as Id, '80.41.1' as Code, 'Обучение водителей автотранспортных средств' as Name, 1892 ParentId, 0 as LastUpdateTick union all
select 1894 as Id, '80.41.2' as Code, 'Обучение летного и мореходного персонала' as Name, 1892 ParentId, 0 as LastUpdateTick union all
select 1895 as Id, '80.42' as Code, 'Образование для взрослых и прочие виды образования, не включенные в другие группировки' as Name, 1869 ParentId, 0 as LastUpdateTick union all
select 1896 as Id, '85' as Code, 'Здравоохранение и предоставление социальных услуг' as Name, 2433 ParentId, 0 as LastUpdateTick union all
select 1897 as Id, '85.1' as Code, 'Деятельность в области здравоохранения' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1898 as Id, '85.11' as Code, 'Деятельность лечебных учреждений' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1899 as Id, '85.11.1' as Code, 'Деятельность больничных учреждений широкого профиля и специализированных' as Name, 1898 ParentId, 0 as LastUpdateTick union all
select 1900 as Id, '85.11.2' as Code, 'Деятельность санаторно-курортных учреждений' as Name, 1898 ParentId, 0 as LastUpdateTick union all
select 1901 as Id, '85.12' as Code, 'Врачебная практика' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1902 as Id, '85.13' as Code, 'Стоматологическая практика' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1903 as Id, '85.14' as Code, 'Прочая деятельность по охране здоровья' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1904 as Id, '85.14.1' as Code, 'Деятельность среднего медицинского персонала' as Name, 1903 ParentId, 0 as LastUpdateTick union all
select 1905 as Id, '85.14.2' as Code, 'Деятельность вспомогательного стоматологического персонала' as Name, 1903 ParentId, 0 as LastUpdateTick union all
select 1906 as Id, '85.14.3' as Code, 'Деятельность медицинских лабораторий' as Name, 1903 ParentId, 0 as LastUpdateTick union all
select 1907 as Id, '85.14.4' as Code, 'Деятельность учреждений скорой медицинской помощи' as Name, 1903 ParentId, 0 as LastUpdateTick union all
select 1908 as Id, '85.14.5' as Code, 'Деятельность учреждений санитарно-эпидемиологической службы' as Name, 1903 ParentId, 0 as LastUpdateTick union all
select 1909 as Id, '85.14.6' as Code, 'Деятельность судебно-медицинской экспертизы' as Name, 1903 ParentId, 0 as LastUpdateTick union all
select 1910 as Id, '85.2' as Code, 'Ветеринарная деятельность' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1911 as Id, '85.20' as Code, 'Ветеринарная деятельность' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1912 as Id, '85.3' as Code, 'Предоставление социальных услуг' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1913 as Id, '85.31' as Code, 'Предоставление социальных услуг с обеспечением проживания' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1914 as Id, '85.32' as Code, 'Предоставление социальных услуг без обеспечения проживания' as Name, 1896 ParentId, 0 as LastUpdateTick union all
select 1915 as Id, '90' as Code, 'Сбор сточных вод, отходов и аналогичная деятельность' as Name, 2434 ParentId, 0 as LastUpdateTick union all
select 1916 as Id, '90.0' as Code, 'Сбор сточных вод, отходов и аналогичная деятельность' as Name, 1915 ParentId, 0 as LastUpdateTick union all
select 1917 as Id, '90.00' as Code, 'Удаление сточных вод, отходов и аналогичная деятельность' as Name, 1916 ParentId, 0 as LastUpdateTick union all
select 1918 as Id, '90.00.1' as Code, 'Удаление и обработка сточных вод' as Name, 1917 ParentId, 0 as LastUpdateTick union all
select 1919 as Id, '90.00.2' as Code, 'Удаление и обработка твердых отходов' as Name, 1917 ParentId, 0 as LastUpdateTick union all
select 1920 as Id, '90.00.3' as Code, 'Уборка территории и аналогичная деятельность' as Name, 1917 ParentId, 0 as LastUpdateTick union all
select 1921 as Id, '91' as Code, 'Деятельность общественных объединений' as Name, 2434 ParentId, 0 as LastUpdateTick union all
select 1922 as Id, '91.1' as Code, 'Деятельность коммерческих, предпринимательских и профессиональных организаций' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1923 as Id, '91.11' as Code, 'Деятельность коммерческих и предпринимательских организаций' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1924 as Id, '91.12' as Code, 'Деятельность профессиональных организаций' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1925 as Id, '91.2' as Code, 'Деятельность профессиональных союзов' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1926 as Id, '91.20' as Code, 'Деятельность профессиональных союзов' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1927 as Id, '91.3' as Code, 'Деятельность прочих общественных объединений' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1928 as Id, '91.31' as Code, 'Деятельность религиозных организаций' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1929 as Id, '91.32' as Code, 'Деятельность политических организаций' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1930 as Id, '91.33' as Code, 'Деятельность прочих общественных организаций, не включенных в другие группировки' as Name, 1921 ParentId, 0 as LastUpdateTick union all
select 1931 as Id, '92' as Code, 'Деятельность по организации отдыха и развлечений, культуры и спорта' as Name, 2434 ParentId, 0 as LastUpdateTick union all
select 1932 as Id, '92.1' as Code, 'Деятельность, связанная с производством, прокатом и показом фильмов' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1933 as Id, '92.11' as Code, 'Производство фильмов' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1934 as Id, '92.12' as Code, 'Прокат фильмов' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1935 as Id, '92.13' as Code, 'Показ фильмов' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1936 as Id, '92.2' as Code, 'Деятельность в области радиовещания и телевидения' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1937 as Id, '92.20' as Code, 'Деятельность в области радиовещания и телевидения' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1938 as Id, '92.3' as Code, 'Прочая зрелищно-развлекательная деятельность' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1939 as Id, '92.31' as Code, 'Деятельность в области искусства' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1940 as Id, '92.31.1' as Code, 'Деятельность в области создания произведений искусства' as Name, 1939 ParentId, 0 as LastUpdateTick union all
select 1941 as Id, '92.31.2' as Code, 'Деятельность в области художественного, литературного и исполнительского творчества' as Name, 1939 ParentId, 0 as LastUpdateTick union all
select 1942 as Id, '92.31.21' as Code, 'Деятельность по организации и постановке театральных и оперных представлений, концертов и прочих сценических выступлений' as Name, 1939 ParentId, 0 as LastUpdateTick union all
select 1943 as Id, '92.31.22' as Code, 'Деятельность актеров, режиссеров, композиторов, художников, скульпторов и прочих представителей творческих профессий, выступающих на индивидуальной основе' as Name, 1939 ParentId, 0 as LastUpdateTick union all
select 1944 as Id, '92.32' as Code, 'Деятельность концертных и театральных залов' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1945 as Id, '92.33' as Code, 'Деятельность ярмарок и парков с аттракционами' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1946 as Id, '92.34' as Code, 'Прочая зрелищно-развлекательная деятельность' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1947 as Id, '92.34.1' as Code, 'Деятельность цирков' as Name, 1946 ParentId, 0 as LastUpdateTick union all
select 1948 as Id, '92.34.2' as Code, 'Деятельность танцплощадок, дискотек, школ танцев' as Name, 1946 ParentId, 0 as LastUpdateTick union all
select 1949 as Id, '92.34.3' as Code, 'Прочая зрелищно-развлекательная деятельность, не включенная в другие группировки' as Name, 1946 ParentId, 0 as LastUpdateTick union all
select 1950 as Id, '92.4' as Code, 'Деятельность информационных агентств' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1951 as Id, '92.40' as Code, 'Деятельность информационных агентств' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1952 as Id, '92.5' as Code, 'Прочая деятельность в области культуры' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1953 as Id, '92.51' as Code, 'Деятельность библиотек, архивов, учреждений клубного типа' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1954 as Id, '92.52' as Code, 'Деятельность музеев и охрана исторических мест и зданий' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1955 as Id, '92.53' as Code, 'Деятельность ботанических садов, зоопарков и заповедников' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1956 as Id, '92.6' as Code, 'Деятельность в области спорта' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1957 as Id, '92.61' as Code, 'Деятельность спортивных объектов' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1958 as Id, '92.62' as Code, 'Прочая деятельность в области спорта' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1959 as Id, '92.7' as Code, 'Прочая деятельность по организации отдыха и развлечений' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1960 as Id, '92.71' as Code, 'Деятельность по организации азартных игр' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1961 as Id, '92.72' as Code, 'Прочая деятельность по организации отдыха и развлечений, не включенная в другие группировки' as Name, 1931 ParentId, 0 as LastUpdateTick union all
select 1962 as Id, '93' as Code, 'Предоставление персональных услуг' as Name, 2434 ParentId, 0 as LastUpdateTick union all
select 1963 as Id, '93.0' as Code, 'Предоставление персональных услуг' as Name, 1962 ParentId, 0 as LastUpdateTick union all
select 1964 as Id, '93.01' as Code, 'Стирка, химическая чистка и окрашивание текстильных и меховых изделий' as Name, 1962 ParentId, 0 as LastUpdateTick union all
select 1965 as Id, '93.02' as Code, 'Предоставление услуг парикмахерскими и салонами красоты' as Name, 1962 ParentId, 0 as LastUpdateTick union all
select 1966 as Id, '93.03' as Code, 'Организация похорон и предоставление связанных с ними услуг' as Name, 1962 ParentId, 0 as LastUpdateTick union all
select 1967 as Id, '93.04' as Code, 'Физкультурно-оздоровительная деятельность' as Name, 1962 ParentId, 0 as LastUpdateTick union all
select 1968 as Id, '93.05' as Code, 'Предоставление прочих персональных услуг' as Name, 1962 ParentId, 0 as LastUpdateTick union all
select 1969 as Id, '95' as Code, 'Деятельность домашних хозяйств с наемными работниками' as Name, 2435 ParentId, 0 as LastUpdateTick union all
select 1970 as Id, '95.0' as Code, 'Деятельность домашних хозяйств с наемными работниками' as Name, 1969 ParentId, 0 as LastUpdateTick union all
select 1971 as Id, '95.00' as Code, 'Деятельность домашних хозяйств с наемными работниками' as Name, 1969 ParentId, 0 as LastUpdateTick union all
select 1972 as Id, '99' as Code, 'Деятельность экстерриториальных организаций' as Name, 2436 ParentId, 0 as LastUpdateTick union all
select 1973 as Id, '99.0' as Code, 'Деятельность экстерриториальных организаций' as Name, 1972 ParentId, 0 as LastUpdateTick union all
select 1974 as Id, '99.00' as Code, 'Деятельность экстерриториальных организаций' as Name, 1972 ParentId, 0 as LastUpdateTick union all
select 1975 as Id, '11.10.14' as Code, 'Добыча горючих (битуминозных) сланцев, битуминозного песка и озокерита' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 1976 as Id, '11.10.21' as Code, 'Добыча природного газа' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 1977 as Id, '11.10.22' as Code, 'Добыча газового конденсата' as Name, 238 ParentId, 0 as LastUpdateTick union all
select 1978 as Id, '13.10.3' as Code, 'Обогащение железных руд' as Name, 260 ParentId, 0 as LastUpdateTick union all
select 1979 as Id, '13.20.21' as Code, 'Добыча и обогащение никелевой руды' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 1980 as Id, '13.20.22' as Code, 'Добыча и обогащение кобальтовой руды' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 1981 as Id, '13.20.91' as Code, 'Добыча и обогащение сурьмяно-ртутных руд' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 1982 as Id, '13.20.92' as Code, 'Добыча и обогащение руд марганцевых' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 1983 as Id, '13.20.93' as Code, 'Добыча и обогащение руд хромовых (хромитовых)' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 1984 as Id, '13.20.94' as Code, 'Добыча и обогащение руд прочих цветных металлов, не включенных в другие группировки' as Name, 264 ParentId, 0 as LastUpdateTick union all
select 1985 as Id, '15.89.4' as Code, 'Переработка меда' as Name, 372 ParentId, 0 as LastUpdateTick union all
select 1986 as Id, '21.12.2' as Code, 'Производство картона' as Name, 491 ParentId, 0 as LastUpdateTick union all
select 1987 as Id, '22.14.1' as Code, 'Издание музыкальных и других звукозаписей на грампластинках, компакт-дисках, видеодисках и магнитных лентах' as Name, 506 ParentId, 0 as LastUpdateTick union all
select 1988 as Id, '22.14.2' as Code, 'Издание нот, в том числе для слепых' as Name, 506 ParentId, 0 as LastUpdateTick union all
select 1989 as Id, '40.11' as Code, 'Производство электроэнергии' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 1990 as Id, '40.11.1' as Code, 'Производство электроэнергии тепловыми электростанциями' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1991 as Id, '40.11.2' as Code, 'Производство электроэнергии гидроэлектростанциями' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1992 as Id, '40.11.3' as Code, 'Производство электроэнергии атомными электростанциями' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1993 as Id, '40.11.4' as Code, 'Производство электроэнергии прочими электростанциями и промышленными блок-станциями' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1994 as Id, '40.11.5' as Code, 'Деятельность по обеспечению работоспособности электростанций' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1995 as Id, '40.11.51' as Code, 'Деятельность по обеспечению работоспособности тепловых электростанций' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1996 as Id, '40.11.52' as Code, 'Деятельность по обеспечению работоспособности гидроэлектростанций' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1997 as Id, '40.11.53' as Code, 'Деятельность по обеспечению работоспособности атомных электростанций' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1998 as Id, '40.11.54' as Code, 'Деятельность по обеспечению работоспособности прочих электростанций и промышленных блок-станций' as Name, 1989 ParentId, 0 as LastUpdateTick union all
select 1999 as Id, '40.12' as Code, 'Передача электроэнергии' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 2000 as Id, '40.13' as Code, 'Распределение электроэнергии и торговля электроэнергией' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 2001 as Id, '40.13.1' as Code, 'Распределение электроэнергии' as Name, 2000 ParentId, 0 as LastUpdateTick union all
select 2002 as Id, '40.13.2' as Code, 'Торговля электроэнергией' as Name, 2000 ParentId, 0 as LastUpdateTick union all
select 2003 as Id, '40.13.3' as Code, 'Деятельность по обеспечению работоспособности электрических сетей' as Name, 2000 ParentId, 0 as LastUpdateTick union all
select 2004 as Id, '40.21' as Code, 'Производство газа' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 2005 as Id, '40.22' as Code, 'Распределение газообразного топлива; торговля газообразным топливом, подаваемым по распределительным сетям' as Name, 997 ParentId, 0 as LastUpdateTick union all
select 2006 as Id, '40.22.1' as Code, 'Распределение газообразного топлива' as Name, 2005 ParentId, 0 as LastUpdateTick union all
select 2007 as Id, '40.22.2' as Code, 'Торговля газообразным топливом, подаваемым по распределительным сетям' as Name, 2005 ParentId, 0 as LastUpdateTick union all
select 2008 as Id, '40.30.6' as Code, 'Торговля паром и горячей водой (тепловой энергией)' as Name, 1018 ParentId, 0 as LastUpdateTick union all
select 2009 as Id, '51.51.4' as Code, 'Оптовая торговля прочим жидким и газообразным топливом' as Name, 1270 ParentId, 0 as LastUpdateTick union all
select 2010 as Id, '51.8' as Code, 'Оптовая торговля машинами и оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2011 as Id, '51.81' as Code, 'Оптовая торговля станками' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2012 as Id, '51.81.1' as Code, 'Оптовая торговля деревообрабатывающими станками' as Name, 2011 ParentId, 0 as LastUpdateTick union all
select 2013 as Id, '51.81.2' as Code, 'Оптовая торговля станками для обработки металлов' as Name, 2011 ParentId, 0 as LastUpdateTick union all
select 2014 as Id, '51.81.3' as Code, 'Оптовая торговля станками для обработки прочих материалов' as Name, 2011 ParentId, 0 as LastUpdateTick union all
select 2015 as Id, '51.82' as Code, 'Оптовая торговля машинами и оборудованием для добычи полезных ископаемых и строительства' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2016 as Id, '51.83' as Code, 'Оптовая торговля машинами и оборудованием для текстильного, швейного и трикотажного производств' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2017 as Id, '51.84' as Code, 'Оптовая торговля компьютерами, периферийными устройствами и программным обеспечением' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2018 as Id, '51.85' as Code, 'Оптовая торговля офисными машинами и оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2019 as Id, '51.85.1' as Code, 'Оптовая торговля офисными машинами' as Name, 2018 ParentId, 0 as LastUpdateTick union all
select 2020 as Id, '51.85.2' as Code, 'Оптовая торговля офисной мебелью' as Name, 2018 ParentId, 0 as LastUpdateTick union all
select 2021 as Id, '51.86' as Code, 'Оптовая торговля прочими электронными деталями (частями) и оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2022 as Id, '51.87' as Code, 'Оптовая торговля прочими машинами и оборудованием' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2023 as Id, '51.87.1' as Code, 'Оптовая торговля транспортными средствами и оборудованием, кроме автомобилей, мотоциклов и велосипедов' as Name, 2022 ParentId, 0 as LastUpdateTick union all
select 2024 as Id, '51.87.2' as Code, 'Оптовая торговля эксплуатационными материалами и принадлежностями машин и оборудования' as Name, 2022 ParentId, 0 as LastUpdateTick union all
select 2025 as Id, '51.87.3' as Code, 'Оптовая торговля подъемно-транспортными машинами и оборудованием' as Name, 2022 ParentId, 0 as LastUpdateTick union all
select 2026 as Id, '51.87.4' as Code, 'Оптовая торговля машинами и оборудованием для производства пищевых продуктов, напитков и табачных изделий' as Name, 2022 ParentId, 0 as LastUpdateTick union all
select 2027 as Id, '51.87.5' as Code, 'Оптовая торговля производственным электрическим оборудованием, машинами, аппаратурой и материалами' as Name, 2022 ParentId, 0 as LastUpdateTick union all
select 2028 as Id, '51.87.6' as Code, 'Оптовая торговля прочими машинами, приборами, аппаратурой и оборудованием общепромышленного и специального назначения' as Name, 2022 ParentId, 0 as LastUpdateTick union all
select 2029 as Id, '51.88' as Code, 'Оптовая торговля машинами, оборудованием и инструментами для сельского хозяйства, включая тракторы' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2030 as Id, '51.88.1' as Code, 'Оптовая торговля тракторами' as Name, 2029 ParentId, 0 as LastUpdateTick union all
select 2031 as Id, '51.88.2' as Code, 'Оптовая торговля прочими машинами, оборудованием и инструментами для сельского и лесного хозяйства' as Name, 2029 ParentId, 0 as LastUpdateTick union all
select 2032 as Id, '51.9' as Code, 'Прочая оптовая торговля' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2033 as Id, '51.90' as Code, 'Прочая оптовая торговля' as Name, 1109 ParentId, 0 as LastUpdateTick union all
select 2034 as Id, '52.46.74' as Code, 'Розничная торговля сборными деревянными строениями' as Name, 1410 ParentId, 0 as LastUpdateTick union all
select 2035 as Id, '52.48.16' as Code, 'Розничная торговля оборудованием электросвязи' as Name, 1425 ParentId, 0 as LastUpdateTick union all
select 2036 as Id, '63.21.11' as Code, 'Деятельность терминалов (железнодорожных станций, перегрузочных товарных станций и т.п.)' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 2037 as Id, '63.21.12' as Code, 'Эксплуатация железнодорожных сооружений' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 2038 as Id, '63.21.13' as Code, 'Техническое обслуживание и мелкий ремонт подвижного состава' as Name, 1561 ParentId, 0 as LastUpdateTick union all
select 2039 as Id, '63.22.11' as Code, 'Деятельность по эксплуатации морских портов, пристаней, шлюзов и т.п., включая деятельность по обслуживанию пассажиров в портах' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2040 as Id, '63.22.12' as Code, 'Лоцманская проводка судов на морском транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2041 as Id, '63.22.13' as Code, 'Деятельность по постановке судов к причалу, осуществление швартования судов в портах на морском транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2042 as Id, '63.22.14' as Code, 'Навигационное обеспечение судоходства на морском транспорте (деятельность береговых служб, радиолокационных станций управления движением судов, обеспечение средствами навигационного оборудования)' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2043 as Id, '63.22.15' as Code, 'Аварийно-спасательная и судоподъемная деятельность на морском транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2044 as Id, '63.22.16' as Code, 'Снабженческое (шипчандлерское) обслуживание судов, включая бункеровку судов топливом, обслуживание судов в период стоянки в портах: агентирование судов, обследовательское (сюрвейерское) обслуживание судов на морском транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2045 as Id, '63.22.17' as Code, 'Производство водолазных работ по обслуживанию морских судов' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2046 as Id, '63.22.18' as Code, 'Деятельность ледокольного флота на морском транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2047 as Id, '63.22.19' as Code, 'Прочая вспомогательная деятельность морского транспорта, не включенная в другие группировки' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2048 as Id, '63.22.21' as Code, 'Деятельность по эксплуатации портов, пристаней, шлюзов и т.п. внутреннего водного транспорта, включая деятельность по обслуживанию пассажиров в портах' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2049 as Id, '63.22.22' as Code, 'Лоцманская проводка судов на внутреннем водном транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2050 as Id, '63.22.23' as Code, 'Деятельность по постановке судов к причалу, осуществление швартования судов в портах на внутреннем водном транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2051 as Id, '63.22.24' as Code, 'Навигационное обеспечение судоходства на внутреннем водном транспорте (деятельность береговых служб, радиолокационных станций управления движением судов, обеспечение средствами навигационного оборудования)' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2052 as Id, '63.22.25' as Code, 'Аварийно-спасательная и судоподъемная деятельность на внутреннем водном транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2053 as Id, '63.22.26' as Code, 'Снабженческое (шипчандлерское) обслуживание судов, включая бункеровку судов топливом, обслуживание судов в период стоянки в портах: агентирование судов, обследовательское (сюрвейерское) обслуживание судов на внутреннем водном транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2054 as Id, '63.22.27' as Code, 'Производство водолазных работ по обслуживанию судов на внутреннем водном транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2055 as Id, '63.22.28' as Code, 'Деятельность ледокольного флота на внутреннем водном транспорте' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2056 as Id, '63.22.29' as Code, 'Прочая вспомогательная деятельность внутреннего водного транспорта, не включенная в другие группировки' as Name, 1568 ParentId, 0 as LastUpdateTick union all
select 2057 as Id, '63.23.61' as Code, 'Содержание и эксплуатация объектов наземной космической инфраструктуры и их составных частей' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2058 as Id, '63.23.62' as Code, 'Предоставление услуг по транспортировке составных частей ракет космического назначения на космодромы (полигоны) и их хранению, а также по доставке эксплуатационного расчета и космонавтов' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2059 as Id, '63.23.63' as Code, 'Предоставление услуг по поставке и транспортированию компонентов ракетного топлива и сжатых газов для ракет космического назначения (ракет) на космодромы (полигоны) и их хранению' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2060 as Id, '63.23.64' as Code, 'Предоставление услуг поисково-спасательных служб' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2061 as Id, '63.23.65' as Code, 'Подготовка космонавтов для работы непосредственно в космическом пространстве' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2062 as Id, '63.23.68' as Code, 'Предоставление услуг, связанных с подготовкой к запуску и пуском ракет космического назначения, не включенных в другие группировки' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2063 as Id, '63.23.69' as Code, 'Прочие вспомогательные услуги, связанные с использованием (эксплуатацией) космических транспортных средств, не включенные в другие группировки' as Name, 1571 ParentId, 0 as LastUpdateTick union all
select 2064 as Id, '64.11.15' as Code, 'Прочая деятельность почтовой связи' as Name, 1588 ParentId, 0 as LastUpdateTick union all
select 2065 as Id, '64.20.4' as Code, 'Деятельность в области передачи данных' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 2066 as Id, '64.20.5' as Code, 'Деятельность в области оказания телематических услуг связи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 2067 as Id, '64.20.6' as Code, 'Деятельность в области кабельного вещания, эфирного вещания и проводного радиовещания' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 2068 as Id, '64.20.7' as Code, 'Прочая деятельность в области электросвязи' as Name, 1600 ParentId, 0 as LastUpdateTick union all
select 2069 as Id, '65.21.1' as Code, 'Финансовый лизинг племенных животных' as Name, 1617 ParentId, 0 as LastUpdateTick union all
select 2070 as Id, '65.21.2' as Code, 'Финансовый лизинг в прочих областях, кроме племенных животных' as Name, 1617 ParentId, 0 as LastUpdateTick union all
select 2071 as Id, '65.22.7' as Code, 'Предоставление услуг по факторингу' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 2072 as Id, '65.22.8' as Code, 'Предоставление услуг по залоговым операциям' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 2073 as Id, '65.22.9' as Code, 'Предоставление услуг доверительного управления активами' as Name, 1618 ParentId, 0 as LastUpdateTick union all
select 2074 as Id, '67.11.14' as Code, 'Деятельность по определению взаимных обязательств (клиринг)' as Name, 1646 ParentId, 0 as LastUpdateTick union all
select 2075 as Id, '55.10' as Code, 'Деятельность гостиниц' as Name, 1465 ParentId, 0 as LastUpdateTick union all
select 2076 as Id, '60.21.24' as Code, 'Внутригородские и пригородные пассажирские перевозки наземным железнодорожным транспортом' as Name, 1493 ParentId, 0 as LastUpdateTick union all
select 2077 as Id, '61.10.21' as Code, 'Деятельность по перевозке сухих грузов морским транспортом' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 2078 as Id, '61.10.22' as Code, 'Деятельность по перевозке наливных грузов морским транспортом' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 2079 as Id, '61.10.29' as Code, 'Деятельность по перевозке прочих грузов морским транспортом' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 2080 as Id, '61.10.31' as Code, 'Аренда морских транспортных средств с экипажем' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 2081 as Id, '61.10.32' as Code, 'Предоставление маневровых услуг на морском транспорте, включая буксировку судов и иных плавучих средств' as Name, 1521 ParentId, 0 as LastUpdateTick union all
select 2082 as Id, '51.36.3' as Code, 'Оптовая торговля хлебом и хлебобулочными изделиями' as Name, 1203 ParentId, 0 as LastUpdateTick union all
select 2083 as Id, '51.36.4' as Code, 'Оптовая торговля мучными кондитерскими изделиями' as Name, 1203 ParentId, 0 as LastUpdateTick union all
select 2084 as Id, '20.10.4' as Code, 'Производство биотоплива (топливные гранулы и брикеты) из отходов деревопереработки' as Name, 464 ParentId, 0 as LastUpdateTick union all
select 2085 as Id, '20.20.11' as Code, 'Производство клееной фанеры' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 2086 as Id, '20.20.12' as Code, 'Производство древесно-стружечных плит' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 2087 as Id, '20.20.13' as Code, 'Производство древесно-волокнистых плит' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 2088 as Id, '20.20.14' as Code, 'Производство клееных щитов' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 2089 as Id, '20.20.19' as Code, 'Производство прочих древесных плит, щитов и панелей' as Name, 470 ParentId, 0 as LastUpdateTick union all
select 2090 as Id, '21.11.1' as Code, 'Производство целлюлозы' as Name, 490 ParentId, 0 as LastUpdateTick union all
select 2091 as Id, '21.11.2' as Code, 'Производство древесной массы' as Name, 490 ParentId, 0 as LastUpdateTick union all
select 2092 as Id, '21.11.3' as Code, 'Производство других волокнистых полуфабрикатов' as Name, 490 ParentId, 0 as LastUpdateTick union all
select 2093 as Id, '75.11.33' as Code, 'Деятельность органов местного самоуправления внутригородских территорий городов федерального значения' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 2094 as Id, '75.11.34' as Code, 'Деятельность органов местного самоуправления городских поселений' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 2095 as Id, '75.11.35' as Code, 'Деятельность органов местного самоуправления сельских поселений' as Name, 1819 ParentId, 0 as LastUpdateTick union all
select 2096 as Id, '75.25.3' as Code, 'Прочая деятельность по обеспечению безопасности в чрезвычайных ситуациях' as Name, 1864 ParentId, 0 as LastUpdateTick union all
select 2097 as Id, '85.20.1' as Code, 'Ветеринарная деятельность в сфере агропромышленного комплекса' as Name, 1911 ParentId, 0 as LastUpdateTick union all
select 2098 as Id, '85.20.2' as Code, 'Прочая ветеринарная деятельность' as Name, 1911 ParentId, 0 as LastUpdateTick union all
select 2099 as Id, '90.01' as Code, 'Сбор и обработка сточных вод' as Name, 1915 ParentId, 0 as LastUpdateTick union all
select 2100 as Id, '90.02' as Code, 'Сбор и обработка прочих отходов' as Name, 1915 ParentId, 0 as LastUpdateTick union all
select 2101 as Id, '90.03' as Code, 'Уборка территории, восстановление после загрязнения и аналогичная деятельность' as Name, 1915 ParentId, 0 as LastUpdateTick union all
select 2102 as Id, '96' as Code, 'Деятельность частных домашних хозяйств по производству товаров для собственного потребления' as Name, 2435 ParentId, 0 as LastUpdateTick union all
select 2103 as Id, '96.0' as Code, 'Деятельность частных домашних хозяйств по производству товаров для собственного потребления' as Name, 2102 ParentId, 0 as LastUpdateTick union all
select 2104 as Id, '96.00' as Code, 'Деятельность частных домашних хозяйств по производству товаров для собственного потребления' as Name, 2102 ParentId, 0 as LastUpdateTick union all
select 2105 as Id, '97' as Code, 'Деятельность частных домашних хозяйств по предоставлению услуг для собственного пользования' as Name, 2435 ParentId, 0 as LastUpdateTick union all
select 2106 as Id, '97.0' as Code, 'Деятельность частных домашних хозяйств по предоставлению услуг для собственного пользования' as Name, 2105 ParentId, 0 as LastUpdateTick union all
select 2107 as Id, '97.00' as Code, 'Деятельность частных домашних хозяйств по предоставлению услуг для собственного пользования' as Name, 2105 ParentId, 0 as LastUpdateTick union all
select 2108 as Id, '71.31.1' as Code, 'Аренда сельскохозяйственных машин, оборудования и племенных животных' as Name, 1706 ParentId, 0 as LastUpdateTick union all
select 2109 as Id, '71.31.11' as Code, 'Аренда сельскохозяйственных машин и оборудования' as Name, 1706 ParentId, 0 as LastUpdateTick union all
select 2110 as Id, '71.31.12' as Code, 'Аренда (лизинг) племенных животных' as Name, 1706 ParentId, 0 as LastUpdateTick union all
select 2111 as Id, '72.21' as Code, 'Разработка программного обеспечения' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 2112 as Id, '72.22' as Code, 'Прочая деятельность по разработке программного обеспечения и консультированию в этой области' as Name, 1729 ParentId, 0 as LastUpdateTick union all
select 2113 as Id, '74.20.16' as Code, 'Деятельность в области промышленного и транспортного дизайна' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 2114 as Id, '74.20.57' as Code, 'Деятельность, связанная с активными воздействиями на метеорологические и геофизические процессы и явления' as Name, 1761 ParentId, 0 as LastUpdateTick union all
select 2115 as Id, '74.85' as Code, 'Предоставление секретарских, редакторских услуг и услуг по переводу' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 2116 as Id, '74.86' as Code, 'Деятельность центров телефонного обслуживания' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 2117 as Id, '74.87' as Code, 'Предоставление прочих деловых услуг, не включенных в другие группировки' as Name, 1747 ParentId, 0 as LastUpdateTick union all
select 2118 as Id, '74.87.1' as Code, 'Взыскание платежей по счетам, оценка платежеспособности в связи с финансовым состоянием или коммерческой практикой частного лица или фирмы' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2119 as Id, '74.87.2' as Code, 'Предоставление посреднических услуг по организации покупки и продажи мелких или средних коммерческих предприятий, включая профессиональную практику' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2120 as Id, '74.87.3' as Code, 'Деятельность по оценке стоимости, кроме оценки, связанной с недвижимым имуществом или страхованием' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2121 as Id, '74.87.4' as Code, 'Деятельность в области дизайна' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2122 as Id, '74.87.5' as Code, 'Предоставление услуг по оформлению помещений, деятельность по организации ярмарок, выставок и конгрессов' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2123 as Id, '74.87.6' as Code, 'Деятельность самостоятельных аукционистов' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2124 as Id, '74.87.7' as Code, 'Деятельность консультантов (кроме консультантов по инженерному проектированию), не включенная в другие группировки' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2125 as Id, '74.87.8' as Code, 'Предоставление прочих деловых услуг' as Name, 2117 ParentId, 0 as LastUpdateTick union all
select 2141 as Id, '21.12.1' as Code, 'Производство бумаги' as Name, 491 ParentId, 0 as LastUpdateTick union all
select 2422 as Id, '' as Code, 'Подраздел АА СЕЛЬСКОЕ ХОЗЯЙСТВО, ОХОТА И ЛЕСНОЕ ХОЗЯЙСТВО' as Name, 100 ParentId, 0 as LastUpdateTick union all
select 2423 as Id, '' as Code, 'Подраздел BА РЫБОЛОВСТВО, РЫБОВОДСТВО' as Name, 101 ParentId, 0 as LastUpdateTick union all
select 2424 as Id, '' as Code, 'Подраздел EА ПРОИЗВОДСТВО И РАСПРЕДЕЛЕНИЕ ЭЛЕКТРОЭНЕРГИИ, ГАЗА И ВОДЫ' as Name, 120 ParentId, 0 as LastUpdateTick union all
select 2425 as Id, '' as Code, 'Подраздел FА СТРОИТЕЛЬСТВО' as Name, 121 ParentId, 0 as LastUpdateTick union all
select 2426 as Id, '' as Code, 'Подраздел GА ОПТОВАЯ И РОЗНИЧНАЯ ТОРГОВЛЯ; РЕМОНТ АВТОТРАНСПОРТНЫХ СРЕДСТВ, МОТОЦИКЛОВ, БЫТОВЫХ ИЗДЕЛИЙ И ПРЕДМЕТОВ ЛИЧНОГО ПОЛЬЗОВАНИЯ' as Name, 122 ParentId, 0 as LastUpdateTick union all
select 2427 as Id, '' as Code, 'Подраздел HА ГОСТИНИЦЫ И РЕСТОРАНЫ' as Name, 123 ParentId, 0 as LastUpdateTick union all
select 2428 as Id, '' as Code, 'Подраздел IА ТРАНСПОРТ И СВЯЗЬ' as Name, 124 ParentId, 0 as LastUpdateTick union all
select 2429 as Id, '' as Code, 'Подраздел JА ФИНАНСОВАЯ ДЕЯТЕЛЬНОСТЬ' as Name, 125 ParentId, 0 as LastUpdateTick union all
select 2430 as Id, '' as Code, 'Подраздел KА ОПЕРАЦИИ С НЕДВИЖИМЫМ ИМУЩЕСТВОМ, АРЕНДА И ПРЕДОСТАВЛЕНИЕ УСЛУГ' as Name, 126 ParentId, 0 as LastUpdateTick union all
select 2431 as Id, '' as Code, 'Подраздел LА ГОСУДАРСТВЕННОЕ УПРАВЛЕНИЕ И ОБЕСПЕЧЕНИЕ ВОЕННОЙ БЕЗОПАСНОСТИ; СОЦИАЛЬНОЕ СТРАХОВАНИЕ' as Name, 127 ParentId, 0 as LastUpdateTick union all
select 2432 as Id, '' as Code, 'Подраздел MА ОБРАЗОВАНИЕ' as Name, 128 ParentId, 0 as LastUpdateTick union all
select 2433 as Id, '' as Code, 'Подраздел NА ЗДРАВООХРАНЕНИЕ И ПРЕДОСТАВЛЕНИЕ СОЦИАЛЬНЫХ УСЛУГ' as Name, 129 ParentId, 0 as LastUpdateTick union all
select 2434 as Id, '' as Code, 'Подраздел OА ПРЕДОСТАВЛЕНИЕ ПРОЧИХ КОММУНАЛЬНЫХ, СОЦИАЛЬНЫХ И ПЕРСОНАЛЬНЫХ УСЛУГ' as Name, 130 ParentId, 0 as LastUpdateTick union all
select 2435 as Id, '' as Code, 'Подраздел PА ДЕЯТЕЛЬНОСТЬ ДОМАШНИХ ХОЗЯЙСТВ' as Name, 131 ParentId, 0 as LastUpdateTick union all
select 2436 as Id, '' as Code, 'Подраздел QА ДЕЯТЕЛЬНОСТЬ ЭКСТЕРРИТОРИАЛЬНЫХ ОРГАНИЗАЦИЙ' as Name, 132 ParentId, 0 as LastUpdateTick union all
select 2437 as Id, '74.60.1' as Code, 'Оценка уязвимости объектов транспортной инфраструктуры и транспортных средств от актов незаконного вмешательства' as Name, 1806 ParentId, 0 as LastUpdateTick union all
select 2438 as Id, '74.60.2' as Code, 'Проведение расследований и обеспечение безопасности, кроме оценки уязвимости объектов транспортной инфраструктуры и транспортных средств от актов незаконного вмешательства' as Name, 1806 ParentId, 0 as LastUpdateTick union all
select 2439 as Id, '34.10.31' as Code, 'Производство автобусов' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 2440 as Id, '34.10.32' as Code, 'Производство троллейбусов' as Name, 899 ParentId, 0 as LastUpdateTick union all
select 2441 as Id, '27.22.4' as Code, 'Производство сварных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 2442 as Id, '27.22.41' as Code, 'Производство сварных водогазопроводных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8638473 as Id, '36.63.8' as Code, 'Производство изделий народных художественных промыслов' as Name, 973 ParentId, 0 as LastUpdateTick union all
select 8790531 as Id, '27.10' as Code, 'Производство чугуна, стали и ферросплавов' as Name, 643 ParentId, 0 as LastUpdateTick union all
select 8790563 as Id, '27.10.1' as Code, 'Производство чугуна и доменных ферросплавов' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790564 as Id, '27.10.2' as Code, 'Производство продуктов прямого восстановления железной руды' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790565 as Id, '27.10.3' as Code, 'Производство ферросплавов, кроме доменных' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790566 as Id, '27.10.4' as Code, 'Производство стали' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790567 as Id, '27.10.5' as Code, 'Производство полуфабрикатов (заготовок) для переката' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790568 as Id, '27.10.6' as Code, 'Производство стального проката горячекатаного и кованого' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790569 as Id, '27.10.7' as Code, 'Производство холоднокатаного плоского проката без защитных покрытий и с защитными покрытиями' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790570 as Id, '27.10.8' as Code, 'Производство железных порошков, прочей металлопродукции из стального проката, не включенной в другие группировки' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790571 as Id, '27.22.1' as Code, 'Производство бесшовных горячекатаных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790572 as Id, '27.22.2' as Code, 'Производство холоднодеформированных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790573 as Id, '27.22.3' as Code, 'Производство нарезных труб и муфт' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790575 as Id, '27.22.5' as Code, 'Производство электросварных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790576 as Id, '27.22.6' as Code, 'Производство муфт' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790582 as Id, '27.43.1' as Code, 'Производство свинца' as Name, 676 ParentId, 0 as LastUpdateTick union all
select 8790583 as Id, '27.43.2' as Code, 'Производство цинка' as Name, 676 ParentId, 0 as LastUpdateTick union all
select 8790584 as Id, '27.43.3' as Code, 'Производство олова' as Name, 676 ParentId, 0 as LastUpdateTick union all
select 8790585 as Id, '27.45.1' as Code, 'Производство никеля' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790586 as Id, '27.45.2' as Code, 'Производство титана' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790587 as Id, '27.45.3' as Code, 'Производство магния' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790588 as Id, '27.45.4' as Code, 'Производство вольфрама' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790589 as Id, '27.45.5' as Code, 'Производство молибдена' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790590 as Id, '27.45.6' as Code, 'Производство кобальта' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790591 as Id, '27.45.7' as Code, 'Производство редких (тантал, ниобий, галлий, германий, иридий) и редкоземельных металлов' as Name, 678 ParentId, 0 as LastUpdateTick union all
select 8790605 as Id, '27.10.61' as Code, 'Производство стального сортового проката горячекатаного и кованого' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790606 as Id, '27.10.62' as Code, 'Производство рельсов' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790607 as Id, '27.10.63' as Code, 'Производство сортового нержавеющего проката' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790608 as Id, '27.10.64' as Code, 'Производство стального горячекатаного листового (плоского) проката' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790609 as Id, '27.10.71' as Code, 'Производство холоднокатаного листового (плоского) проката без защитных покрытий' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790610 as Id, '27.10.72' as Code, 'Производство холоднокатаного плоского проката с защитными покрытиями' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790611 as Id, '27.10.73' as Code, 'Производство холоднокатаного листового (плоского) нержавеющего проката' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790612 as Id, '27.10.74' as Code, 'Производство листовых штрипсов' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790613 as Id, '27.10.81' as Code, 'Производство железных порошков' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790614 as Id, '27.10.82' as Code, 'Производство изделий из стального проката для верхнего строения железнодорожного пути' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790615 as Id, '27.10.83' as Code, 'Производство профилей и конструкций шпунтового типа из стального проката' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790616 as Id, '27.10.84' as Code, 'Производство холоднотянутого проката, включая подшипниковый' as Name, 8790531 ParentId, 0 as LastUpdateTick union all
select 8790617 as Id, '27.22.11' as Code, 'Производство бесшовных горячекатаных нержавеющих труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790618 as Id, '27.22.12' as Code, 'Производство бесшовных горячекатаных для котлов высокого давления труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790619 as Id, '27.22.13' as Code, 'Производство бесшовных горячекатаных подшипниковых труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790620 as Id, '27.22.19' as Code, 'Производство бесшовных горячекатаных прочих труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790621 as Id, '27.22.21' as Code, 'Производство холоднодеформированных тянутых труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790622 as Id, '27.22.22' as Code, 'Производство холоднодеформированных нержавеющих труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790623 as Id, '27.22.23' as Code, 'Производство холоднодеформированных для котлов высокого давления труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790624 as Id, '27.22.24' as Code, 'Производство холоднодеформированных подшипниковых труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790625 as Id, '27.22.25' as Code, 'Производство холоднодеформированных тонкостенных углеродистых труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790626 as Id, '27.22.26' as Code, 'Производство холоднодеформированных тонкостенных легированных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790627 as Id, '27.22.31' as Code, 'Производство нарезных бурильных труб и муфт' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790628 as Id, '27.22.32' as Code, 'Производство нарезных обсадных труб и муфт' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790629 as Id, '27.22.33' as Code, 'Производство нарезных насосно-компрессорных труб и муфт' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790630 as Id, '27.22.34' as Code, 'Производство нарезных электросварных труб и муфт' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790632 as Id, '27.22.42' as Code, 'Производство свертнопаяных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790633 as Id, '27.22.51' as Code, 'Производство электросварных с применением сварки под слоем флюса труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790634 as Id, '27.22.52' as Code, 'Производство электросварных с нагревом токами высокой частоты тонкостенных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790635 as Id, '27.22.53' as Code, 'Производство электросварных с нагревом токами высокой частоты нефтепроводных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790636 as Id, '27.22.54' as Code, 'Производство электросварных с нагревом токами высокой частоты профильных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790637 as Id, '27.22.55' as Code, 'Производство электросварных с нагревом токами высокой частоты водогазопроводных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790638 as Id, '27.22.56' as Code, 'Производство электросварных нержавеющих труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790639 as Id, '27.22.57' as Code, 'Производство электросварных нержавеющих холоднодеформированных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790640 as Id, '27.22.61' as Code, 'Производство муфт для водогазопроводных труб' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790641 as Id, '27.22.62' as Code, 'Производство муфт под сварку' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790642 as Id, '27.22.63' as Code, 'Производство прочих соединительных деталей под сварку' as Name, 656 ParentId, 0 as LastUpdateTick union all
select 8790673 as Id, '29.41' as Code, 'Производство переносных ручных инструментов с механическим приводом' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 8790674 as Id, '29.42' as Code, 'Производство прочих станков для обработки металлов' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 8790675 as Id, '29.43' as Code, 'Производство прочих станков, не включенных в другие группировки' as Name, 730 ParentId, 0 as LastUpdateTick union all
select 8790718 as Id, '29.42.1' as Code, 'Производство металлорежущих станков' as Name, 8790674 ParentId, 0 as LastUpdateTick union all
select 8790719 as Id, '29.42.2' as Code, 'Производство кузнечно-прессового оборудования' as Name, 8790674 ParentId, 0 as LastUpdateTick union all
select 8790720 as Id, '29.43.1' as Code, 'Производство деревообрабатывающего оборудования' as Name, 8790675 ParentId, 0 as LastUpdateTick union all
select 8790721 as Id, '29.43.2' as Code, 'Производство оборудования для пайки, сварки и резки, машин и аппаратов для поверхностной термообработки и газотермического напыления' as Name, 8790675 ParentId, 0 as LastUpdateTick union all
select 8790795 as Id, '32.20.4' as Code, 'Производство аппаратуры для передачи данных' as Name, 856 ParentId, 0 as LastUpdateTick union all
select 8790877 as Id, '35.30.49' as Code, 'Производство прочих космических объектов, не включенных в другие группировки' as Name, 928 ParentId, 0 as LastUpdateTick union all
select 8791853 as Id, '75.23.5' as Code, 'Деятельность Следственного комитета Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 8791884 as Id, '75.23.51' as Code, 'Деятельность центрального аппарата Следственного комитета Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick union all
select 8791885 as Id, '75.23.52' as Code, 'Деятельность Главного военного следственного управления, главных следственных управлений и следственных управлений Следственного комитета Российской Федерации по субъектам Российской Федерации (в том числе подразделений указанных управлений по административным округам) и приравненных к ним специализированных (в том числе военных) следственных управлений и следственных отделов Следственного комитета Российской Федерации' as Name, 1842 ParentId, 0 as LastUpdateTick


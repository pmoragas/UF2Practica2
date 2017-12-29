# UF2Practica2

## Enunciat

Disposeu d’un arxiu JSON (persons.json) que us podeu descarregar del moodle, que conté una relació de persones amb els següents camps: name, surname, gender, company, mail i country. La pràctica consistirà a treballar amb aquest arxiu, mostrant informació combinant TPL i async/await.

## Lliurament mínim (nota màxima 6)

Es demana que feu una aplicació d’escriptori (Winforms o WPF) que llegeixi del JSON els diferents països i que els mostri i que seleccionant sobre un determinat país, mostri en un llistat el nom (name i surname) i correu electrònic dels individus d’aquell país.

La cerca la fareu d’una forma seqüencial i utilitzant les TPL (parallel for o parallel foreach), tenint cadascuna de les opcions el seu ListBox i el seu botó corresponent.

Un cop mostreu els resultats, heu de mostrar el temps que ha trigat a fer l’acció (teniu més informació a les orientacions).

Els events s’han d’implementar de forma asíncrona, de tal manera que la interfície no quedi bloquejada.

## Ampliacions i millores

* Permetre fer seleccions no només per país, sinó també per gènere o companyia.

* Incloure la possibilitats de cerques obertes per nom, companyia, etc. utilitzant PLINQ.

* Millores que creieu aportin usabilitat (bloqueig de botons, mostrar informació mentre es fa la cerca, etc.)

## Orientacions

Per llegir el JSON el més senzill és utilitzar el paquet de Newtonsoft que podeu descarregar amb nuget, tot i que també existeixen alternatives natives del framework. Aquí teniu una sèrie de links on teniu informació de com utilitzar-lo:

[newtonsoft](http://www.newtonsoft.com/json/help/html/SerializingJSON.htm)

[JSON C# fácil](https://www.campusmvp.es/recursos/post/Como-usar-JSON-en-NET-facilmente.aspx)

[JSON con C#](http://javierescobar.net/como-implementar-json-con-csharp/)

Per calcular el temps que es triga a fer una operació:

```csharp

Stopwatch clock = new Stopwacth();

clock.Restart();

// Aquí aniria el codi de la crida al mètode

clock.Stop();

textBox1.Text = clock.Elapsed.TotalSeconds.ToString() + "segons";
```

## Lliurament

Caldrà lliurar el codi, fent presentació i defensa davant del professorat, a més de penjar el projecte .ZIP al moodle.



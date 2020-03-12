all: Gamma.svg ErrorFun.svg Lngamma.svg  LngammaLong.svg

main.exe: main.cs math.dll
	mcs $< -out:$@ -reference:math.dll

main-lngamma.exe: main-lngamma.cs
	mcs $< -out:$@ -reference:math.dll

main-lngammaLong.exe: main-lngammaLong.cs
	mcs $< -out:$@ -reference:math.dll

math.dll: gamma.cs error-function.cs
	mcs $^ -target:library -out:$@

out.gamma.txt out.error.txt: main.exe
	mono $< > out.gamma.txt 2> out.error.txt

out.lngamma.txt: main-lngamma.exe
	mono $< > $@

out.lngammaLong.txt: main-lngammaLong.exe
	mono $< > $@

out.gamma.test.txt: gamma.c
	tcc -run $< > $@

out.lngamma.test.txt: lngamma.c
	tcc -run $< > $@

out.lngammaLong.test.txt: lngammaLong.c
	tcc -run $< > $@


Gamma.svg: gamma.gpi out.gamma.txt out.gamma.test.txt
	gnuplot $<

ErrorFun.svg: error.gpi out.error.txt error.test.txt
	gnuplot $<

Lngamma.svg: lngamma.gpi out.lngamma.txt  out.lngamma.test.txt 
	gnuplot $<

LngammaLong.svg: lngammaLong.gpi out.lngammaLong.txt  out.lngammaLong.test.txt 
	gnuplot $<

.PHONEY:clean
clean:
	rm --force *.dll *.exe out* *.svg


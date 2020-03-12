CSFLAGS = -warn:2 -optimize-

.PHONEY:all
all : out.txt
	cat $<

out.txt : main.exe
	mono $< > $@
main.exe : main.cs cmath.dll
	mcs $(CSFLAGS) -reference:cmath.dll -out:$@ $<

CDIR=../../lib/complex
cmath.dll : $(CDIR)/cmath.cs $(CDIR)/complex.cs
	mcs $(CSFLAGS) -target:library -out:$@ $^
	cp $(CDIR)/cmath.dll .

clean:
	rm -f out.txt main.exe cmath.dll
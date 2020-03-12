CSFLAGS = -warn:2 -optimize-
.PHONEY:all
all : out.txt
	cat $<

out.txt : main.exe
	mono $< > $@
main.exe : main.cs approx.dll
	mcs $(CSFLAGS) -reference:approx.dll -out:$@ $<

approx.dll : approx.cs
	mcs $(CSFLAGS) -target:library -out:$@ $<

clean:
	rm -f out.txt main.exe approx.dll
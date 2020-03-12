all: stdin_out.txt cmdline_out.txt read_file.exe

stdin_out.txt: read_stdin.exe input.txt
	mono $< < $(word 2,$^) > $@

cmdline_out.txt: read_cmdline.exe input.txt
	mono $< $$(cat $(word 2,$^)) > $@

read_stdin.exe:stdin.cs
	mcs $< -out:$@

read_cmdline.exe:cmdline.cs
	mcs $< -out:$@

read_file.exe:file.cs
	mcs $< -out:$@

clean: 
	$(RM) *.exe out*

input.txt: Makefile
	/bin/echo -e '1\n2\n3\n4\n5' > $@
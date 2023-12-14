#include <fcntl.h>
#include <stdio.h>

int main(int argc, char** argv)
{
	int rs;
	char tens;
	char units;
	int fd;
	char* buf;
	int nc;
	int cur;

	if (argc < 2)
	{
		printf("Usage: %s inputFile\n", argv[0]);
		return -1;
	}

	rs = 0; /* running sum */
	tens = '\0';
	units = '\0';

	buf = (char*)malloc(1024);
	if ((fd = open(argv[1], O_RDONLY)) == 0)
	{
		printf("Unreadable input file %s\n", argv[1]);
		return -1;
	}
	while ((nc = read(fd, buf, 1024)) > 0)
	{
		cur = 0;
		while (cur < nc)
		{
			if (buf[cur] >= '0' && buf[cur] <= '9')
			{
				if (tens == '\0')
				{
					tens = buf[cur];
				}
				else
				{
					units = buf[cur];
				}
			}
			if (buf[cur] == '\n')
			{
				if (tens == '\0') tens = '0';
				if (units == '\0') units = tens;
				rs += (tens - '0') * 10 + (units - '0');
				units = tens = '\0';
			}
			cur++;
		}
	}
	close(fd);
	printf("%d\n", rs);
	return 0;
}

export function convertToEasternTime(timeString: string) {
    const isoString = timeString.replace(' ', 'T') + 'Z';
    const date = new Date(isoString);

    const options: Intl.DateTimeFormatOptions = {
        timeZone: 'America/New_York',
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        hour12: false,
    };

    const formatter = new Intl.DateTimeFormat('en-US', options!);

    const parts = formatter.formatToParts(date);
    const dateParts: { [key: string]: string } = {};

    parts.forEach(({ type, value }) => {
        dateParts[type] = value;
    });

    const easternTime = `${dateParts.year}-${dateParts.month}-${dateParts.day} ${dateParts.hour}:${dateParts.minute}:${dateParts.second}`;

    return easternTime;
}

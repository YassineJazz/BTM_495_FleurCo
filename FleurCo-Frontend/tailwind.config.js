/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  daisyui: {
    themes: [
      {
        pastel: {
          ...require("daisyui/src/theming/themes")["pastel"],
          "--rounded-btn": "0.75rem",
          primary: "#abeaea",
          secondary: "#abeaea",
          neutral: "#abeaea"

        }
      },]

  },
  themes: {
    extend: {}
  },
  plugins: [require("daisyui")]
}

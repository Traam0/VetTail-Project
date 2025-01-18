/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Views/**/*.cshtml',
        './Views/**/*.html',
        './wwwroot/**/*.html'
    ],
  theme: {
      extend: {
          colors: {
              primary: "#8731d2",
              "primary-content": "#ffffff",
              "primary-dark": "#6d25ab",
              "primary-light": "#9f5bdb",

              secondary: "#31d287",
              "secondary-content": "#010302",
              "secondary-dark": "#25ab6d",
              "secondary-light": "#5bdb9f",

              background: "#f0ecf4",
              foreground: "#fbfafc",
              border: "#e0d7e7",

              copy: "#271d30",
              "copy-light": "#684d80",
              "copy-lighter": "#8e70a9",

              success: "#31d231",
              warning: "#d2d231",
              error: "#d23131",

              "success-content": "#010301",
              "warning-content": "#030301",
              "error-content": "#ffffff"
          },
      },
  },
  plugins: [],
}


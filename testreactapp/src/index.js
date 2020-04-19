import React from "react";
import ReactDOM from "react-dom";
import App from "./component/app.component";

import {I18nextProvider} from 'react-i18next';
import i18next from 'i18next';

import common_ru from './translations/ru/common.json';
import common_en from "./translations/en/common.json";

i18next.init({
  interpolation: { escapeValue: false },
  lng: 'en', 
  resources: {
      en: {
          common: common_en
      },
      ru: {
          common: common_ru
      },
  },
});


ReactDOM.render(
  <I18nextProvider i18n={i18next}>
    <App />
  </I18nextProvider>,
  document.querySelector("#root")
);
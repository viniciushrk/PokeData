FROM node:16

# Create app directory
WORKDIR /usr/src/app/


COPY ["package.json", "package-lock.json*", "./"]

COPY package*.json /usr/src/app/

RUN npm install

RUN npm install pm2@latest -g

COPY . .

EXPOSE 3000

CMD [ "pm2-runtime", "index.js" ]
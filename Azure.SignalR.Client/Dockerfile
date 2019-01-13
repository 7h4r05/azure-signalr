FROM node:8.12.0-alpine as build-stage

WORKDIR /app
COPY package*.json /app/
RUN npm install
COPY ./ /app/

RUN npm run build -- --output-path=./dist/out 

FROM nginx:1.13.9-alpine
COPY --from=build-stage /app/dist/out/ /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]